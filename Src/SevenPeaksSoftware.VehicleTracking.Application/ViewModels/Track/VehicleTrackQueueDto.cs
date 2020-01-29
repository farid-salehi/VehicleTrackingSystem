using System;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track
{
    public class VehicleTrackQueueDto
    {
        public double Latitude { get; set; }
        public double Longitudes { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        /// <summary>
        /// store the current time to use it in execute time
        /// </summary>
        public DateTimeOffset CreatedDateTimeOffset { get; set; } = DateTimeOffset.Now;
    }
}
