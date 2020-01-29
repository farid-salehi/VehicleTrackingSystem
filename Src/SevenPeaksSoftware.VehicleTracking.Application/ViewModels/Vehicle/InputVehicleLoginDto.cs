using System.ComponentModel.DataAnnotations;
using SevenPeaksSoftware.VehicleTracking.Domain.Enums;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle
{
    public class InputVehicleLoginDto
    {
        [Required]
        [MaxLength((int)ModelRestrictionsEnum.VehicleRestrictionsEnum.VehicleRegistrationNumberMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.VehicleRestrictionsEnum.VehicleRegistrationNumberMinCharLength)]
        public string VehicleRegistrationNumber { get; set; }
        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMinCharLength)]
        public string Password { get; set; }
    }
}
