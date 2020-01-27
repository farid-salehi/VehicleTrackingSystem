using System;

namespace SevenPeaksSoftware.VehicleTracking.WebApi.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}