using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleTrackingDbContext _dbContext;

        public UnitOfWork(VehicleTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
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
