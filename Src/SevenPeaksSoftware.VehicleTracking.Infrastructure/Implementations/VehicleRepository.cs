
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class VehicleRepository : Repository<VehicleModel>, IVehicleRepository
    {
        public VehicleRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }

    }
}
