using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleTrackingDbContext _dbContext;


        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IVehicleRepository VehicleRepository { get; }
        public IVehicleTrackRepository VehicleTrackRepository { get; }

        public UnitOfWork(VehicleTrackingDbContext dbContext,
            IUserRepository userRepository,
            IRoleRepository roleRepository, 
            IUserRoleRepository userRoleRepository,
            IVehicleRepository vehicleRepository, 
            IVehicleTrackRepository vehicleTrackRepository)
        {
            _dbContext = dbContext;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            UserRoleRepository = userRoleRepository;
            VehicleRepository = vehicleRepository;
            VehicleTrackRepository = vehicleTrackRepository;
        }



        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
