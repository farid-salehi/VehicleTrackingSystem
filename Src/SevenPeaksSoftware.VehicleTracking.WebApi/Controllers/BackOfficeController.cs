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
        private readonly IRoleService _roleService;

        public BackOfficeController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync
            ([FromBody] InputLoginDto user, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _userService.LoginAsync(user, cancellationToken)).ResponseHandler();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshTokenAsync
            ([FromBody] TokenDto token, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _userService.RefreshTokenAsync(token, cancellationToken)).ResponseHandler();
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


        [HttpGet]
        public async Task<IActionResult> GetRoleListAsync
            (CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _roleService.GetRoleListAsync(cancellationToken)).ResponseHandler();
        }


    }
}