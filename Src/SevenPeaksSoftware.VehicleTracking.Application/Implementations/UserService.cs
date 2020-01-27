using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SevenPeaksSoftware.VehicleTracking.Application.ApplicationUtils;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.Settings;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VehicleTrackingSettings _settings;

        public UserService(IUnitOfWork unitOfWork,
            IOptions<VehicleTrackingSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _settings = settings.Value;
        }

        public async Task<ResponseDto<OutputAddUserDto>> AddUserAsync(InputAddUserDto user, CancellationToken cancellationToken)
        {
            if ((await _unitOfWork.UserRepository.GetUserAsync(user.Username,
                    cancellationToken)) != null)
            {
                return ResponseDto<OutputAddUserDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Conflict, "The username already exists.");
            }

            var newUser = Mapper.Map<InputAddUserDto, UserModel>(user);
            var hashPasswordArray = user.Password.HashPassword();
            newUser.Password = hashPasswordArray.Item1;
            newUser.Salt = hashPasswordArray.Item2;

            foreach (var roleId in user.RoleIdList)
            {
                var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId, cancellationToken);
                if (role == null)
                {
                    return ResponseDto<OutputAddUserDto>.UnsuccessfulResponse
                        (ResponseEnums.ErrorEnum.BadRequest, $"RoleId: {roleId} is not defined.");
                }

                await _unitOfWork.UserRoleRepository.AddAsync(new UserRoleModel()
                {
                    UserInfo = newUser,
                    RoleId = roleId
                }, cancellationToken);
            }
            
            await _unitOfWork.CommitAsync(cancellationToken);

            return ResponseDto<OutputAddUserDto>.SuccessfulResponse(new OutputAddUserDto()
            {
                UserId = newUser.UserId
            });
        }


        public async Task<ResponseDto<TokenDto>> LoginAsync(InputLoginDto user, CancellationToken cancellationToken)
        {
            var userModel = await _unitOfWork.UserRepository.
                GetUserAsync(user.Username, cancellationToken);

            if (userModel == null)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "User not found.");
            }

            var roleList = await _unitOfWork.UserRoleRepository.GetUserRoleListAsync(userModel.UserId, cancellationToken);
            if (roleList.Count == 0)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "This user has no role.");
            }

            var hashedPassword = user.Password.HashPassword(userModel.Salt);
            if (!string.Equals(hashedPassword, userModel.Password))
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse(ResponseEnums.ErrorEnum.Forbidden,
                    "The username or password is incorrect.");
            }

            userModel.RefreshToken = IdentityUtils.GenerateRefreshToken();

            await _unitOfWork.CommitAsync(cancellationToken);
            var token = new TokenDto()
            {
                AccessToken = GenerateJwt(userModel, roleList),
                RefreshToken = userModel.RefreshToken

            };
            return ResponseDto<TokenDto>.SuccessfulResponse(token);
        }

        public async Task<ResponseDto<TokenDto>> RefreshTokenAsync(TokenDto token, CancellationToken cancellationToken)
        {
            var principal = token.AccessToken.GetPrincipalFromExpiredToken(_settings.IdentitySettings.SecretKey);
            if (principal == null)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "Invalid access token");
            }

            var userModel = await _unitOfWork.UserRepository.
                GetUserAsync(principal.Identity.Name, cancellationToken);

            if (userModel == null)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "User not found.");
            }

            var roleList = await _unitOfWork.UserRoleRepository.GetUserRoleListAsync(userModel.UserId, cancellationToken);
            if (roleList.Count == 0)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "This user has no role.");
            }

            if (!userModel.RefreshToken.Equals(token.RefreshToken))
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "Invalid refresh token");
            }

            userModel.RefreshToken = IdentityUtils.GenerateRefreshToken();

            await _unitOfWork.CommitAsync(cancellationToken);

            var newToken = new TokenDto()
            {
                AccessToken = GenerateJwt(userModel, roleList),
                RefreshToken = userModel.RefreshToken

            };
            return ResponseDto<TokenDto>.SuccessfulResponse(newToken);
        }

        public async Task<ResponseDto<ICollection<OutputUserDto>>> GetUserListAsync
            (LimitOffsetOrderByDto limitOffset, CancellationToken cancellationToken)
        {
            var userList =
                (await _unitOfWork.UserRepository.GetUserListAsync
                    (limitOffset.Limit, limitOffset.Offset, limitOffset.OrderByDescending, cancellationToken));

            var result = userList.Select(Mapper.Map<UserModel, OutputUserDto>).ToList();

            if (result.Count == 0)
            {
                return ResponseDto<ICollection<OutputUserDto>>.UnsuccessfulResponse(
                    ResponseEnums.ErrorEnum.NoContent, " ");
            }
            return ResponseDto<ICollection<OutputUserDto>>.SuccessfulResponse(result);
        }

        private string GenerateJwt(UserModel user, IEnumerable<RoleModel> roleList)
        {
            var claimList = roleList.Select(x => new Claim(ClaimTypes.Role, x.RoleName)).ToList();
            claimList.Add(new Claim(ClaimTypes.Name, user.Username));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.IdentitySettings.SecretKey));
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken
            (
                issuer: _settings.IdentitySettings.Issuer,
                audience: _settings.IdentitySettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_settings.IdentitySettings.ExpiredTimeInMinute),
                claims: claimList.ToArray(),
                signingCredentials: signInCred
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
