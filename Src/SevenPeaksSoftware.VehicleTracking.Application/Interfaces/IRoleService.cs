using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Role;

namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseDto<ICollection<RoleDto>>> GetRoleListAsync(CancellationToken cancellationToken);
    }
}
