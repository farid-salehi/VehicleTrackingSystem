using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations.InMemoryImplementations
{
    public class InMemoryRepository : IInMemoryRepository
    {
        public ITaskQueueInMemoryRepository TaskQueueInMemoryRepository { get; }
        public IVehicleTrackInMemoryRepository VehicleTrackInMemoryRepository { get; }


        public InMemoryRepository( ITaskQueueInMemoryRepository taskQueueInMemoryRepository
            , IVehicleTrackInMemoryRepository vehicleTrackInMemoryRepository)
        {
            TaskQueueInMemoryRepository = taskQueueInMemoryRepository;
            VehicleTrackInMemoryRepository = vehicleTrackInMemoryRepository;
        }

    }
}
