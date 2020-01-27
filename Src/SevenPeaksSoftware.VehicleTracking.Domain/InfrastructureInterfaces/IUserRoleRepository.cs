
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IUserRoleRepository : IRepository<UserRoleModel>
    {
        Task<ICollection<RoleModel>> GetUserRoleListAsync
            (int userId, CancellationToken cancellationToken);
    }
}
