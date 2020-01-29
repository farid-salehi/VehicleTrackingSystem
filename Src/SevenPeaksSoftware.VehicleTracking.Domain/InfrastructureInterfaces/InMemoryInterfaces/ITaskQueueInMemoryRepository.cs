using System.Threading.Tasks;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces
{
    public interface ITaskQueueInMemoryRepository
    {
        Task<bool> QueueTaskAsync(string queueName, string objectString);
    }
}
