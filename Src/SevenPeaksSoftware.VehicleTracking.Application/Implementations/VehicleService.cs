using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SevenPeaksSoftware.VehicleTracking.Application.ApplicationUtils;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.Settings;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VehicleTrackingSettings _settings;



        public VehicleService(IUnitOfWork unitOfWork,
            IOptions<VehicleTrackingSettings> settings)
        {
            _unitOfWork = unitOfWork;
            _settings = settings.Value;
        }

        public async Task<ResponseDto<OutputRegisterVehicleDto>> RegisterVehicleAsync(InputVehicleDto vehicle,
            CancellationToken cancellationToken)
        {
            if ((await _unitOfWork.VehicleRepository.GetVehicle(vehicle.VehicleRegistrationNumber,
                    cancellationToken)) != null)
            {
                return ResponseDto<OutputRegisterVehicleDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Conflict, "The vehicle already exists.");
            }

            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var hashPasswordArray = password.HashPassword();

            var newVehicle = new VehicleModel()
            {
                VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
                Password = hashPasswordArray.Item1,
                Salt = hashPasswordArray.Item2
            };

            await _unitOfWork.VehicleRepository.AddAsync(newVehicle,
                cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ResponseDto<OutputRegisterVehicleDto>.SuccessfulResponse(new OutputRegisterVehicleDto()
            {
                  VehicleRegisterNumber = newVehicle.VehicleRegistrationNumber,
                  Password = password
            });
        }

        public async Task<ResponseDto<OutputRegisterVehicleDto>> GetVehicleNewPassword(InputVehicleDto vehicle,
            CancellationToken cancellationToken)
        {
            var vehicleModel = await _unitOfWork.VehicleRepository
                .GetVehicle(vehicle.VehicleRegistrationNumber,
                    cancellationToken);
            if (vehicleModel == null)
            {
                return ResponseDto<OutputRegisterVehicleDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Conflict, "Vehicle is not found.");
            }


            _unitOfWork.VehicleRepository.Remove(vehicleModel);

            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var hashPasswordArray = password.HashPassword();

            var newVehicle = new VehicleModel()
            {
                VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
                Password = hashPasswordArray.Item1,
                Salt = hashPasswordArray.Item2
            };

            await _unitOfWork.VehicleRepository.AddAsync(newVehicle,
                cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ResponseDto<OutputRegisterVehicleDto>.SuccessfulResponse(new OutputRegisterVehicleDto()
            {
                VehicleRegisterNumber = newVehicle.VehicleRegistrationNumber,
                Password = password
            });
        }

        public async Task<ResponseDto<ICollection<VehicleDto>>> GetRegisteredVehicleListAsync
            (LimitOffsetOrderByDto limitOffset, CancellationToken cancellationToken)
        {
            var result =
                (await _unitOfWork.VehicleRepository.GetVehicleList
                    (limitOffset.Limit, limitOffset.Offset, limitOffset.OrderByDescending, cancellationToken))
                .Select(v =>
                    new VehicleDto()
                    {
                        VehicleId = v.VehicleId,
                        VehicleRegistrationNumber = v.VehicleRegistrationNumber
                    }).ToList();

            if (result.Count == 0)
            {
                return ResponseDto<ICollection<VehicleDto>>.UnsuccessfulResponse(
                    ResponseEnums.ErrorEnum.NoContent, " ");
            }
            return ResponseDto<ICollection<VehicleDto>>.SuccessfulResponse(result);
        }

        public async Task<ResponseDto<TokenDto>> LoginAsync(InputVehicleLoginDto vehicle, CancellationToken cancellationToken)
        {
            var vehicleModel = await _unitOfWork.VehicleRepository.
                GetVehicle(vehicle.VehicleRegistrationNumber, cancellationToken);

            if (vehicleModel == null)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "Vehicle not found.");
            }



            var hashedPassword = vehicle.Password.HashPassword(vehicleModel.Salt);
            if (!string.Equals(hashedPassword, vehicleModel.Password))
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse(ResponseEnums.ErrorEnum.Forbidden, "The vehicle number or password is incorrect.");
            }

            vehicleModel.RefreshToken = IdentityUtils.GenerateRefreshToken();
            await _unitOfWork.CommitAsync(cancellationToken);

            var token = new TokenDto()
            {
                AccessToken = GenerateJwt(vehicleModel),
                RefreshToken = vehicleModel.RefreshToken
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

            var vehicleModel = await _unitOfWork.VehicleRepository.
                GetVehicle(principal.Identity.Name, cancellationToken);

            if (vehicleModel == null)
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Forbidden, "Vehicle not found.");
            }


            if (!vehicleModel.RefreshToken.Equals(token.RefreshToken))
            {
                return ResponseDto<TokenDto>.UnsuccessfulResponse(ResponseEnums.ErrorEnum.Forbidden, "Invalid refresh token");
            }

            vehicleModel.RefreshToken = IdentityUtils.GenerateRefreshToken();
            await _unitOfWork.CommitAsync(cancellationToken);

            var newToken = new TokenDto()
            {
                AccessToken = GenerateJwt(vehicleModel),
                RefreshToken = vehicleModel.RefreshToken
            };
            return ResponseDto<TokenDto>.SuccessfulResponse(newToken);
        }

        private string GenerateJwt(VehicleModel vehicle)
        {
            var claimList = new[]
            {
                new Claim(ClaimTypes.Name, vehicle.VehicleRegistrationNumber),
                new Claim(ClaimTypes.Role, ApplicationEnums.RoleEnum.GpsNavigator.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.IdentitySettings.SecretKey));
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken
            (
                issuer: _settings.IdentitySettings.Issuer,
                audience: _settings.IdentitySettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_settings.IdentitySettings.ExpiredTimeInMinute),
                claims: claimList.ToArray(),
                signingCredentials: signInCred
            );

            var tokenString = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

    }
}
