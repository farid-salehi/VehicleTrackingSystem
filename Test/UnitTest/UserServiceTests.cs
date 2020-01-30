using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Options;
using Moq;
using SevenPeaksSoftware.VehicleTracking.Application.Implementations;
using Xunit;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.Settings;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;

namespace UnitTests
{
    public class UserServiceTests
    {
        private IUserService userService;
        private readonly IOptions<VehicleTrackingSettings> _settings;
        public UserServiceTests()
        {
            _settings = Options.Create(new VehicleTrackingSettings());
        }


        [Theory]
        [InlineData("Admin")]
        public void Add_user_should_reject_duplicated_userName(string username)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.UserRepository
                    .GetUserAsync(It.IsAny<string>(), new CancellationToken()))
                .ReturnsAsync(new UserModel() { Username = username });

            userService = new UserService(mockUnitOfWork.Object, _settings);

            var result = (userService.AddUserAsync(new InputAddUserDto()
            {
                Username = username
            },
                new CancellationToken())).Result.Success;
            Assert.False(result);
        }


        [Theory]
        [InlineData("Admin", "1234")]
        public void Login_should_reject_incorrect_password(string username, string password)
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.UserRepository
                    .GetUserAsync(It.IsAny<string>(), new CancellationToken()))
                .ReturnsAsync(new UserModel() { Username = username, Password = "QAZ", Salt = new byte[0] });

            mockUnitOfWork.Setup(x => x.UserRoleRepository
                    .GetUserRoleListAsync(It.IsAny<int>(), new CancellationToken()))
                .ReturnsAsync(new List<RoleModel>() { new RoleModel() { RoleName = username, RoleId = 1 } });



            userService = new UserService(mockUnitOfWork.Object, _settings);

            var result = (userService.LoginAsync(new InputLoginDto() { Username = username, Password = password },
                new CancellationToken())).Result.Success;

            Assert.False(result);
        }
    }
}
