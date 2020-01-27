
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IVehicleRepository : IRepository<VehicleModel>
    {
        Task<VehicleModel> GetVehicle
            (string vehicleRegistrationNumber, CancellationToken cancellationToken);
    }
}
