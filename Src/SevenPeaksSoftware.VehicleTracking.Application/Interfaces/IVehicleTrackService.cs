using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track;


namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface IVehicleTrackService
    {
        Task<ResponseDto<bool>> TrackQueueAsync
        (InputTrackDto track, string vehicleRegistrationNumber,
            CancellationToken cancellationToken);

        Task TrackAsync(CancellationToken cancellationToken);

        Task<ResponseDto<OutputGetVehicleCurrentLocation>> GetVehicleCurrentLocationAsync
            (InputGetVehicleCurrentLocation vehicle, CancellationToken cancellationToken);


        Task<ResponseDto<ICollection<PointDateTimeDto>>> GetVehicleRouteAsync
            (InputGetVehicleRouteDto vehicle, CancellationToken cancellationToken);


    }
}
