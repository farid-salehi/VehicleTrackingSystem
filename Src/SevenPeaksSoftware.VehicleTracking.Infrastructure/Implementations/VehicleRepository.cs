
using System.Collections.Generic;
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

        public async Task<ICollection<VehicleModel>> GetVehicleList
            (int limit, int offset, bool orderByDescending, CancellationToken cancellationToken)
        {
            if (orderByDescending)
            {
                return await DbContext.Vehicles
                    .Where(v => !v.IsDeleted)
                    .OrderByDescending(v => v.CreatedDateTime)
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync(cancellationToken);
            }
            return await DbContext.Vehicles
                .Where(v => !v.IsDeleted)
                .OrderBy(v => v.CreatedDateTime)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }

    }
}
