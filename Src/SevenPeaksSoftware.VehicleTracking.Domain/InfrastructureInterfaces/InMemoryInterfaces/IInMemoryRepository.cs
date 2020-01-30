namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces
{
    public interface IInMemoryRepository
    {
        ITaskQueueInMemoryRepository  TaskQueueInMemoryRepository  { get; }
        IVehicleTrackInMemoryRepository VehicleTrackInMemoryRepository { get; }
    }
}
