
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle;

namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<ResponseDto<OutputRegisterVehicleDto>> RegisterVehicleAsync(InputVehicleDto vehicle,
            CancellationToken cancellationToken);

        Task<ResponseDto<OutputRegisterVehicleDto>> GetVehicleNewPassword(InputVehicleDto vehicle,
            CancellationToken cancellationToken);

        Task<ResponseDto<ICollection<VehicleDto>>> GetRegisteredVehicleListAsync
            (LimitOffsetOrderByDto limitOffset, CancellationToken cancellationToken);

        Task<ResponseDto<TokenDto>> LoginAsync(InputVehicleLoginDto vehicle, 
            CancellationToken cancellationToken);

        Task<ResponseDto<TokenDto>> RefreshTokenAsync(TokenDto token, 
            CancellationToken cancellationToken);
    }
}
