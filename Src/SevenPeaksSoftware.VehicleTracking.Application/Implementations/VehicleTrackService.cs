using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;


namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class VehicleTrackService : IVehicleTrackService
    {
        private readonly IInMemoryRepository _inMemoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VehicleTrackService( IInMemoryRepository inMemoryRepository,
            IUnitOfWork unitOfWork)
        {
            _inMemoryRepository = inMemoryRepository;
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// for better performance, store track data in redis queue in this step
        /// and a taskManager listen on the queue.
        /// at the moment a track data push to the queue, the task manager execute it.
        /// </summary>
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

        /// <summary>
        /// 1-pop the first track data from queue
        /// 2-check if the current location changed
        /// (each time store the last location of device in redis for better performance when it wants to check the last location),
        /// update location in db and redis  else do nothing to store db clean and light
        /// </summary>
        public async Task TrackAsync(CancellationToken cancellationToken)
        {

            
            var objectString = await _inMemoryRepository.TaskQueueInMemoryRepository
                .DeQueueTaskAsync(ApplicationEnums.TaskQueueEnum.TrackVehicleQueue.ToString());

            if (string.IsNullOrEmpty(objectString))
            {
                return;
            }
            var track = JsonConvert.DeserializeObject<VehicleTrackQueueDto>(objectString);

            if (string.IsNullOrEmpty(track.VehicleRegistrationNumber))
            {
                return;
            }
            var vehicle = await _unitOfWork.VehicleRepository
                .GetVehicle(track.VehicleRegistrationNumber, cancellationToken);

            if (vehicle == null)
            {
                return;
            }

            var point = await _inMemoryRepository.VehicleTrackInMemoryRepository
                .GetVehicleCurrentLocation(track.VehicleRegistrationNumber);


            if (point == null
                || point.Longitudes.CompareTo(track.Longitudes) != 0
                || point.Latitude.CompareTo(track.Latitude) != 0)
            {
                await _inMemoryRepository.VehicleTrackInMemoryRepository
                    .AddOrUpdateVehicleLocationAsync
                        (track.VehicleRegistrationNumber, track.Latitude, track.Longitudes);
            }
            else
            {
                return;
            }

            var trackVehicle = new VehicleTrackModel()
            {
                Latitude = track.Latitude,
                Longitudes = track.Longitudes,
                VehicleId = vehicle.VehicleId
            };

            await _unitOfWork.VehicleTrackRepository.AddAsync
                (trackVehicle, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

        }


    }
}
