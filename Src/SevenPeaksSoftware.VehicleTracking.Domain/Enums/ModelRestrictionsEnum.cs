
namespace SevenPeaksSoftware.VehicleTracking.Domain.Enums
{
    public class ModelRestrictionsEnum
    {
        public enum UserRestrictionsEnum
        {
            FirstNameMaxCharLength =50,
            FirstNameMinCharLength = 3, 

            LastNameMaxCharLength = 50,
            LastNameMinCharLength = 3,

            UsernameMaxCharLength = 50,
            UsernameMinCharLength = 3,

            EmailMaxCharLength = 320,
   

            MobileNumberMaxCharLength = 15,

            HashPasswordNumberMaxCharLength = 512,
            RefreshTokenMaxCharLength = 512,
            PasswordNumberMaxCharLength = 30,
            PasswordNumberMinCharLength = 8,
        }

        public enum VehicleRestrictionsEnum
        {
            VehicleNameMaxCharLength = 50,
            VehicleNameMinCharLength = 2,
            VehicleRegistrationNumberMinCharLength = 2,
            VehicleRegistrationNumberMaxCharLength = 70
        }


        public enum RoleRestrictionsEnum
        {
            RoleNameMaxCharLength = 50,
            RoleNameMinCharLength = 2,
        }
    }
}
