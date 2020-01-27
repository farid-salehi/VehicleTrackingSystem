using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;

namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto<OutputAddUserDto>> AddUserAsync(InputAddUserDto user, CancellationToken cancellationToken);
        Task<ResponseDto<TokenDto>> LoginAsync(InputLoginDto user, CancellationToken cancellationToken);
        Task<ResponseDto<TokenDto>> RefreshTokenAsync(TokenDto token, CancellationToken cancellationToken);
    }
}
