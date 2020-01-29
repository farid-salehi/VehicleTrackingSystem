using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;


namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class VehicleTrackService : IVehicleTrackService
    {
        private readonly IInMemoryRepository _inMemoryRepository;

        public VehicleTrackService( IInMemoryRepository inMemoryRepository)
        {
            _inMemoryRepository = inMemoryRepository;
        }



        public async Task<ResponseDto<bool>> TrackQueueAsync
        (InputTrackDto track, string vehicleRegistrationNumber,
            CancellationToken cancellationToken)
        {
            var jsonObject = JsonConvert.SerializeObject(new VehicleTrackQueueDto()
            {
                Latitude = track.Latitude,
                Longitudes = track.Longitudes,
                VehicleRegistrationNumber = vehicleRegistrationNumber
            });
           await _inMemoryRepository.TaskQueueInMemoryRepository
                .QueueTaskAsync(ApplicationEnums.TaskQueueEnum.TrackVehicleQueue.ToString(),
                jsonObject);

           return ResponseDto<bool>.SuccessfulResponse(true);
        }


       
    }
}
