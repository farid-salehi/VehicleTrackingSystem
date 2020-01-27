
namespace SevenPeaksSoftware.VehicleTracking.Domain.Models
{
    public class UserRoleModel: BaseModel
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public virtual UserModel UserInfo { get; set; }
        public int RoleId { get; set; }
        public virtual RoleModel RoleInfo { get; set; }

    }
}
