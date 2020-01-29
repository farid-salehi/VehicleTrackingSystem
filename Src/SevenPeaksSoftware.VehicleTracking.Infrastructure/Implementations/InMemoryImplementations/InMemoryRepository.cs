using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations.InMemoryImplementations
{
    public class InMemoryRepository : IInMemoryRepository
    {
        public ITaskQueueInMemoryRepository TaskQueueInMemoryRepository { get; }



        public InMemoryRepository( ITaskQueueInMemoryRepository taskQueueInMemoryRepository)
        {
            TaskQueueInMemoryRepository = taskQueueInMemoryRepository;
        }

    }
}
