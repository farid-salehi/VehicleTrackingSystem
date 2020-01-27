using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.WebApi.WebApiUtils;

namespace SevenPeaksSoftware.VehicleTracking.WebApi.Controllers
{
    [Route("api/v1/[Controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class BackOfficeController : Controller
    {

        private readonly IUserService _userService;

        public BackOfficeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync
            ([FromBody]InputAddUserDto user, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _userService.AddUserAsync(user, cancellationToken)).ResponseHandler();
        }
    }
}