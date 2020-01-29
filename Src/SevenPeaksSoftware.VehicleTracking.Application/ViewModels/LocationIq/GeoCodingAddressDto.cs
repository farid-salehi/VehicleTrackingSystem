
using Newtonsoft.Json;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.LocationIq
{
    public class GeoCodingAddressDto
    {
        [JsonProperty("road")]
        public string Road { get; set; }
        [JsonProperty("neighbourhood")]
        public string Neighbourhood { get; set; }
        [JsonProperty("suburb")]
        public string Suburb { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("county")]
        public string County { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("postcode")]
        public string Postcode { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}
