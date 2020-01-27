
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class UserRoleRepository : Repository<UserRoleModel>, IUserRoleRepository
    {
        public UserRoleRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }

    }
}
