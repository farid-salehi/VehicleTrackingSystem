
namespace SevenPeaksSoftware.VehicleTracking.Application.Settings
{
    public class IdentitySettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiredTimeInMinute { get; set; }
    }
}
