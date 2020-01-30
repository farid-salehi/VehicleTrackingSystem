using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.LocationIq;

namespace SevenPeaksSoftware.VehicleTracking.Application.Interfaces
{
    public interface ILocationIqThirdPartyService
    {
        Task<OutputReverseGeoCodingDto> ReverseGeoCodingAsync
            (double latitude, double longitudes, CancellationToken cancellationToken);
    }
}
