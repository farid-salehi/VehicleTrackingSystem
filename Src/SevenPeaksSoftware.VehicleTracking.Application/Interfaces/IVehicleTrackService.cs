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


    }
}
