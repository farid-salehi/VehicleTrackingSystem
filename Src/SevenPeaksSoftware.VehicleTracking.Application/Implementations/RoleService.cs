using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Role;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;


namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<ICollection<RoleDto>>> GetRoleListAsync(CancellationToken cancellationToken)
        {
          var result =   (await _unitOfWork.RoleRepository.GetAllAsync(cancellationToken))
                .Select(r => new RoleDto()
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                }).ToList();

          if (result.Count ==0)
          {
              return ResponseDto<ICollection<RoleDto>>
                  .UnsuccessfulResponse(ResponseEnums.ErrorEnum.NoContent, "No role was found.");
          }

          return ResponseDto<ICollection<RoleDto>>.SuccessfulResponse(result);
        }
    }
}
