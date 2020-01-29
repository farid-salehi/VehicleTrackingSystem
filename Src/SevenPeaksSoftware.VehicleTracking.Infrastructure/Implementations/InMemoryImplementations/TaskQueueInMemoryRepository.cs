﻿using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;
using StackExchange.Redis;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations.InMemoryImplementations
{
    public class TaskQueueInMemoryRepository : ITaskQueueInMemoryRepository
    {
        private readonly IDatabase _db;
        protected readonly IRedisConnectionFactory ConnectionFactory;

        public TaskQueueInMemoryRepository(IRedisConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            _db = ConnectionFactory.Connection().GetDatabase();
        }

        public async Task<bool> QueueTaskAsync(string queueName, string objectString)
        {
           await _db.ListLeftPushAsync
                (queueName, new RedisValue[] {objectString}, CommandFlags.FireAndForget);
            return true;
        }
    }
}
