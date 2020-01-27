namespace SevenPeaksSoftware.VehicleTracking.Domain.Models
{
    public class VehicleTrackModel : BaseModel
    {
        public long VehicleTrackId { get; set; }
        public int VehicleId { get; set; }
        public virtual VehicleModel VehicleInfo { get; set; }
        public double Latitude { get; set; }
        public double Longitudes { get; set; }
    }
}
