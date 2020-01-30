using System;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track
{
    public class InputGetVehicleRouteDto
    {
        public string VehicleRegistrationNumber { get; set; }
        public DateTimeOffset StartDateTimeOffset { get; set; }
        public DateTimeOffset EndDateTimeOffset { get; set; }
    }
}
