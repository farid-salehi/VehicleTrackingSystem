
using StackExchange.Redis;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer Connection();
    }
}
