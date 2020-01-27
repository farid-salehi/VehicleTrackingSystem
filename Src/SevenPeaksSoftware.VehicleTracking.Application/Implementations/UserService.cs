using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SevenPeaksSoftware.VehicleTracking.Application.ApplicationUtils;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
       
    }
}
