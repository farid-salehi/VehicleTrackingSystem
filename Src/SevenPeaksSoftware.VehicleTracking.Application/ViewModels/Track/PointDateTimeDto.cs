using System;
using System.Collections.Generic;
using System.Text;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track
{
    public class PointDateTimeDto
    {
        public double Latitude { get; set; }
        public double Longitudes { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }
    }
}
