﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle;
using SevenPeaksSoftware.VehicleTracking.WebApi.WebApiUtils;

namespace SevenPeaksSoftware.VehicleTracking.WebApi.Controllers
{
    [Route("api/v1/[Controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class BackOfficeController : Controller
    {

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleTrackService _vehicleTrackService;

        public BackOfficeController(IUserService userService
            , IRoleService roleService
            , IVehicleService vehicleService
            , IVehicleTrackService vehicleTrackService)
        {
            _userService = userService;
            _roleService = roleService;
            _vehicleService = vehicleService;
            _vehicleTrackService = vehicleTrackService;
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


        [HttpPost]
        public async Task<IActionResult> GetUserListAsync
            ([FromBody] LimitOffsetOrderByDto limitOffset, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _userService.GetUserListAsync(limitOffset, cancellationToken)).ResponseHandler();
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


        [HttpPost]
        public async Task<IActionResult> RegisterVehicleAsync
            ([FromBody] InputVehicleDto vehicle, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleService.RegisterVehicleAsync
                (vehicle, cancellationToken)).ResponseHandler();
        }



        [HttpPost]
        public async Task<IActionResult> GetVehicleNewPasswordAsync
            ([FromBody] InputVehicleDto vehicle, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleService.GetVehicleNewPassword
                (vehicle, cancellationToken)).ResponseHandler();
        }

        [HttpPost]
        public async Task<IActionResult> GetVehicleListAsync
            ([FromBody] LimitOffsetOrderByDto limitOffset, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleService.GetRegisteredVehicleListAsync
                (limitOffset, cancellationToken)).ResponseHandler();
        }


        [HttpPost]
        public async Task<IActionResult> GetVehicleCurrentLocationAsync
            ([FromBody] InputGetVehicleCurrentLocation vehicle, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleTrackService.GetVehicleCurrentLocationAsync
                (vehicle, cancellationToken)).ResponseHandler();
        }

        [HttpPost]
        public async Task<IActionResult> GetVehicleRouteAsync
            ([FromBody] InputGetVehicleRouteDto vehicle, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (ModelState.BadRequestErrorHandler()).ResponseHandler();
            }
            return (await _vehicleTrackService.GetVehicleRouteAsync
                (vehicle, cancellationToken)).ResponseHandler();
        }
    }
}