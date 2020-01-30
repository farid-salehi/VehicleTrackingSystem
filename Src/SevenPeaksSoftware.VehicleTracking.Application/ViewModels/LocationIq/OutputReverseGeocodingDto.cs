using System.Collections.Generic;
using Newtonsoft.Json;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.LocationIq
{
    public class OutputReverseGeoCodingDto
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; } = "";
        [JsonProperty("licence")]
        public string License { get; set; } = "";
        [JsonProperty("osm_type")]
        public string OsmType { get; set; } = "";
        [JsonProperty("osm_id")]
        public string OsmId { get; set; } = "";
        [JsonProperty("lat")]
        public string Lat { get; set; } = "";
        [JsonProperty("lon")]
        public string Lon { get; set; } = "";
        [JsonProperty("display_name")]
        public string DisplayName { get; set; } = "";
        [JsonProperty("address")]
        public GeoCodingAddressDto Address { get; set; } 
        [JsonProperty("boundingbox")]
        public List<string> BoundingBox { get; set; } 
    }
}
