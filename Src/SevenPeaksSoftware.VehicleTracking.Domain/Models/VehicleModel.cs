
using System.Collections.Generic;

namespace SevenPeaksSoftware.VehicleTracking.Domain.Models
{
    public class VehicleModel : BaseModel
    {
        public int VehicleId { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public string RefreshToken { get; set; }
        public virtual ICollection<VehicleTrackModel> VehicleTrackList { get; set; }
            = new List<VehicleTrackModel>();

    }
}
