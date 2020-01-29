using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Infrastructure.Settings;
using StackExchange.Redis;


namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations.InMemoryImplementations
{
    public class VehicleTrackInMemoryRepository: IVehicleTrackInMemoryRepository
    {
        private readonly IDatabase _db;
        protected readonly IRedisConnectionFactory ConnectionFactory;
        private readonly InfrastructureSettings _settings;

        public VehicleTrackInMemoryRepository(IRedisConnectionFactory connectionFactory,
            IOptions<InfrastructureSettings> settings)
        {
            ConnectionFactory = connectionFactory;
            _settings = settings.Value;
            _db = ConnectionFactory.Connection().GetDatabase();
        }


        public async Task<bool> AddOrUpdateVehicleLocationAsync
            (string vehicleRegistrationNumber, double latitude, double longitudes)
        {
            var point = new PointDto()
            {
                Latitude = latitude,
                Longitudes = longitudes
            };
            var jsonObject = JsonConvert.SerializeObject(point);
            return await _db.StringSetAsync(vehicleRegistrationNumber, jsonObject);

        }

        public async Task<PointDto> GetVehicleCurrentLocation
            (string vehicleRegistrationNumber)
        {
            try
            {
                var jsonObject = (await _db.StringGetAsync(vehicleRegistrationNumber));
                return jsonObject.IsNull ? null : JsonConvert.DeserializeObject<PointDto>(jsonObject);
            }
            catch (Exception)
            {
                return null;
            }
       
        }


    }
}
