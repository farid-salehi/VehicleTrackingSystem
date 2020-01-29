using System.Threading.Tasks;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces
{
    public interface ITaskQueueInMemoryRepository
    {
        Task<bool> QueueTaskAsync(string queueName, string objectString);
        Task<string> DeQueueTaskAsync(string queueName);
        Task<long> TaskCount(string queueName);
    }
}
