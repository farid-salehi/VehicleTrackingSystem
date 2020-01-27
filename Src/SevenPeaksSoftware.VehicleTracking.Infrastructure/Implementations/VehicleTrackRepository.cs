
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class VehicleTrackRepository : Repository<VehicleTrackModel>, IVehicleTrackRepository
    {
        public VehicleTrackRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {

        }
    }
}
