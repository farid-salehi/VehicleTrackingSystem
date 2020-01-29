using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle;
using SevenPeaksSoftware.VehicleTracking.WebApi.WebApiUtils;

namespace SevenPeaksSoftware.VehicleTracking.WebApi.Controllers
{
    [Route("api/v1/[Controller]/[action]")]
    [Authorize(Roles = "GpsNavigator")]
    public class GpsNavigatorController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public GpsNavigatorController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VehicleLoginAsync
            ([FromBody] InputVehicleLoginDto vehicle, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleService.LoginAsync
                (vehicle, cancellationToken)).ResponseHandler();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VehicleRefreshTokenAsync
            ([FromBody] TokenDto token, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleService.RefreshTokenAsync
                (token, cancellationToken)).ResponseHandler();
        }

    }
}