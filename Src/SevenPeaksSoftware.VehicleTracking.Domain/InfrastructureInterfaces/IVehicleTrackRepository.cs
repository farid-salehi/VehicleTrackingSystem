

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IVehicleTrackRepository : IRepository<VehicleTrackModel>
    {
        Task<ICollection<VehicleTrackModel>> GetVehicleRoteAsync
        (string vehicleRegistrationNumber, DateTimeOffset startDateTimeOffset,
            DateTimeOffset endDateTimeOffset, CancellationToken cancellationToken);
    }
}
