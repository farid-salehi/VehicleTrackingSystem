
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class VehicleTrackRepository : Repository<VehicleTrackModel>, IVehicleTrackRepository
    {
        public VehicleTrackRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<ICollection<VehicleTrackModel>> GetVehicleRoteAsync
            (string vehicleRegisterNumber, DateTimeOffset startDateTimeOffset,
                DateTimeOffset endDateTimeOffset, CancellationToken cancellationToken)
        {
            //try to get route by time
            var route = await DbContext.VehicleTracks
                .Include(t => t.VehicleInfo)
                .Where(t => !t.IsDeleted
                            && t.VehicleInfo.VehicleRegistrationNumber.ToLower()
                                .Equals(vehicleRegisterNumber)
                            && t.CreatedDateTime >= startDateTimeOffset
                            && t.CreatedDateTime <= endDateTimeOffset)
                .OrderByDescending(t => t.CreatedDateTime)
                .ToListAsync(cancellationToken);
            if (route.Count != 0)
            {
                return route;
            }

            //if could not find any point in the time, it means it was stopped
            //try to find last position of vehicle before time
             var lastLocation =  (await DbContext.VehicleTracks
                .Include(t => t.VehicleInfo)
                .Include(t => t.VehicleInfo)
                .Where(t => !t.IsDeleted
                            && t.VehicleInfo.VehicleRegistrationNumber.ToLower()
                                .Equals(vehicleRegisterNumber)
                            && t.CreatedDateTime < startDateTimeOffset)
                .OrderByDescending(t => t.CreatedDateTime)
                .FirstOrDefaultAsync(cancellationToken));
             lastLocation.CreatedDateTime = endDateTimeOffset;

            route.Add(lastLocation);

            return route;

        }
    }

}
