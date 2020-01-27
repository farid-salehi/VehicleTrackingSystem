
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SevenPeaksSoftware.VehicleTracking.Application.ApplicationUtils
{
    public static class ApplicationUtils
    {
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var validMailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string NormalizeMobileNumber(this string MobileNumber, string countryCode)
        {
            countryCode = countryCode.Replace("00", "+");
            MobileNumber = MobileNumber.Replace("(", "");
            MobileNumber = MobileNumber.Replace(")", "");
            MobileNumber = MobileNumber.Replace("-", "");
            MobileNumber = MobileNumber.Trim();
            MobileNumber = MobileNumber.Replace(".", "");
            MobileNumber = MobileNumber.TrimStart('0');
            return countryCode + "-" + MobileNumber;
        }

        public static bool IsMobileNumberValid(this string MobileNumber)
        {
            var regex = new Regex(@"\+\d{1,4}\-\d{3,15}");
            if (regex.IsMatch(MobileNumber))
            {
                return true;
            }

            return false;
        }

        public static bool IsUserNameValid(this string username)
        {
            var regex = new Regex(@"^[a-z0-9_-]{3,16}$");
            if (regex.IsMatch(username))
            {
                return true;
            }
            return false;
        }

    }
}
