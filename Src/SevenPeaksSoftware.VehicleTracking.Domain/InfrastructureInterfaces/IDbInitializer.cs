
namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IDbInitializer
    {
        void Migrate();
        void Seed();
    }
}
