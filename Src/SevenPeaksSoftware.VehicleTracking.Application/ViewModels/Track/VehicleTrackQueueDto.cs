using System;
using System.Collections.Generic;
using System.Text;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track
{
    public class VehicleTrackQueueDto
    {
        public double Latitude { get; set; }
        public double Longitudes { get; set; }
        public string VehicleRegistrationNumber { get; set; }
    }
}
