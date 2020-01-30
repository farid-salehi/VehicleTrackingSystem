using Microsoft.Extensions.Options;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;
using SevenPeaksSoftware.VehicleTracking.Infrastructure.Settings;
using StackExchange.Redis;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations.InMemoryImplementations
{
    public class RedisConnectFactory : IRedisConnectionFactory
    {
        private readonly ConnectionMultiplexer _connection;
        private readonly InfrastructureSettings _settings;

        public RedisConnectFactory(IOptions<InfrastructureSettings> settings)
        {
            _settings = settings.Value;
            _connection = 
                ConnectionMultiplexer.Connect(_settings.RedisSettings.RedisConnection);

            
        }

        public ConnectionMultiplexer Connection()
        {
           return  _connection;
        }
    }
}
