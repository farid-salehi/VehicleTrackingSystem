using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;

namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto<OutputAddUserDto>> AddUserAsync(InputAddUserDto user, CancellationToken cancellationToken);
    }
}
