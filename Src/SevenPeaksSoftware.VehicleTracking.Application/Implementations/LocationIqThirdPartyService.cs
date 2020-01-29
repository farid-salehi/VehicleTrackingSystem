using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.Settings;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.LocationIq;

namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class LocationIqThirdPartyService : ILocationIqThirdPartyService
    {
        private readonly VehicleTrackingSettings _settings;
        private readonly HttpClient _client;

        public LocationIqThirdPartyService(IOptions<VehicleTrackingSettings> settings)
        {
            _settings = settings.Value;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_settings.LocationIqSettings.BaseUrl),
                Timeout = new TimeSpan(0, 0, 10)
            };
        }

        public async Task<OutputReverseGeoCodingDto> ReverseGeoCodingAsync
            (double latitude, double longitudes, CancellationToken cancellationToken)
        {
            try
            {
            
                var query = $"?key={_settings.LocationIqSettings.Token}&lat={latitude}&lon={longitudes}&format=json";
                var response = await (await _client.GetAsync(query, cancellationToken))
                    .Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OutputReverseGeoCodingDto>(response);
            }
            catch (Exception)
            {
                //log
                return null;
            }

        }
    }
}
