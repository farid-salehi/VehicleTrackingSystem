using System.Collections.Generic;

namespace SevenPeaksSoftware.VehicleTracking.Domain.Models
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } = "";
        public string MobileNumber { get; set; } = "";
        public byte[] Salt { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public virtual ICollection<UserRoleModel> UserRoleList { get; set; }
            = new List<UserRoleModel>();
    }
}
