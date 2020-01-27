
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IUnitOfWork : IDisposable
    {

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
