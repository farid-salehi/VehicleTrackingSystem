
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class VehicleRepository : Repository<VehicleModel>, IVehicleRepository
    {
        public VehicleRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<VehicleModel> GetVehicle
            (string vehicleRegistrationNumber, CancellationToken cancellationToken)
        {
            return await DbContext.Vehicles
                .Where(v => !v.IsDeleted
                            && v.VehicleRegistrationNumber.ToLower().Equals(vehicleRegistrationNumber.ToLower()))
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
