
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;


namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }

    }
}
