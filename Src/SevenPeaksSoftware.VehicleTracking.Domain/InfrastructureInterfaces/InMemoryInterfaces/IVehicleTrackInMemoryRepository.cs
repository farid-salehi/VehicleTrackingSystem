using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.ViewModels;


namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces
{
    public interface IVehicleTrackInMemoryRepository
    {
        Task<bool> AddOrUpdateVehicleLocationAsync
            (string vehicleRegistrationNumber, double latitude, double longitudes);

        Task<PointDto> GetVehicleCurrentLocation
            (string vehicleRegistrationNumber);

    }
}
