
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle;

namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<ResponseDto<OutputRegisterVehicleDto>> RegisterVehicleAsync(InputVehicleDto vehicle,
            CancellationToken cancellationToken);

        Task<ResponseDto<OutputRegisterVehicleDto>> GetVehicleNewPassword(InputVehicleDto vehicle,
            CancellationToken cancellationToken);
    }
}
