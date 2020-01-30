using System.Collections.Generic;

namespace SevenPeaksSoftware.VehicleTracking.Domain.Models
{
    public class RoleModel : BaseModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UserRoleModel> UserRoleList { get; set; }
            = new List<UserRoleModel>();
    }
}
