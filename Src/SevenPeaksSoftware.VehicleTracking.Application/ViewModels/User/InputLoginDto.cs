using System.ComponentModel.DataAnnotations;
using SevenPeaksSoftware.VehicleTracking.Domain.Enums;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User
{
    public class InputLoginDto
    {
        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.UsernameMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.UsernameMinCharLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMinCharLength)]
        public string Password { get; set; }
    }
}
