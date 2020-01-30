
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;


namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class RoleRepository : Repository<RoleModel>, IRoleRepository
    {
        public RoleRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }

        
    }
}
