using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SevenPeaksSoftware.VehicleTracking.Application.ApplicationUtils;
using SevenPeaksSoftware.VehicleTracking.Domain.Enums;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User
{
    public class InputAddUserDto : IValidatableObject
    {
        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.FirstNameMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.FirstNameMinCharLength)]
        public string FirstName { get; set; }


        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.LastNameMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.LastNameMinCharLength)]
        public string LastName { get; set; }


        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.UsernameMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.UsernameMinCharLength)]
        public string Username { get; set; }

        [Required]
        public List<int> RoleIdList { get; set; }

        [MaxLength(6)]
       public string CountryCode { get; set; } = "+66";


        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.MobileNumberMaxCharLength)]
        public string MobileNumber { get; set; }


        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.EmailMaxCharLength)]
        public string Email { get; set; }


        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMinCharLength)]
        public string Password { get; set; }


        [Required]
        [MaxLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMaxCharLength)]
        [MinLength((int)ModelRestrictionsEnum.UserRestrictionsEnum.PasswordNumberMinCharLength)]
        public string ConfirmPassword { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (!string.Equals(Password, ConfirmPassword))
            {
                yield return new ValidationResult("Your password and confirmation password do not match.");
            }

            if (!string.IsNullOrEmpty(Email) && !Email.IsValidEmail())
            {
                yield return new ValidationResult( "Please Enter a correct Email Address.");
                
            }

            if (!string.IsNullOrEmpty(MobileNumber))
            {

                MobileNumber = MobileNumber.NormalizeMobileNumber(CountryCode);
                if (!MobileNumber.IsMobileNumberValid())
                {
                    yield return new ValidationResult("Please Enter a correct Mobile Number (123456789). and Correct Country Code(0066).");
                }

            }

            if (!Username.IsUserNameValid())
            {
                    yield return new ValidationResult("Invalid Username, you can use letters, numbers, - and _");

            }
        }
    }

}
