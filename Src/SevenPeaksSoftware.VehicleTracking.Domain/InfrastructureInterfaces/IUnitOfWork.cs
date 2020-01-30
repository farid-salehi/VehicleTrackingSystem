
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IVehicleRepository VehicleRepository { get; }
        IVehicleTrackRepository VehicleTrackRepository { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
