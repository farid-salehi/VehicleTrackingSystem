using System.ComponentModel.DataAnnotations;
using SevenPeaksSoftware.VehicleTracking.Domain.Enums;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle
{
    public class InputVehicleDto
    {
   
        [Required]
        [MaxLength((int)ModelRestrictionsEnum.VehicleRestrictionsEnum.VehicleRegistrationNumberMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.VehicleRestrictionsEnum.VehicleRegistrationNumberMinCharLength)]
        public string VehicleRegistrationNumber { get; set; }
    }
}
