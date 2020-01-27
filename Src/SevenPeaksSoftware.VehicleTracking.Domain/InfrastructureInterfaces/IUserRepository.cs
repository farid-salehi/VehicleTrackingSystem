
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<UserModel> GetUserAsync(string userName, CancellationToken cancellationToken);

        Task<ICollection<UserModel>> GetUserListAsync
            (int limit, int offset, bool orderByDescending, CancellationToken cancellationToken);
    }
}
