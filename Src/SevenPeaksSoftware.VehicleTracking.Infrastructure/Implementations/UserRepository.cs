
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;


namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<UserModel> GetUserAsync(string userName, CancellationToken cancellationToken)
        {
            return await DbContext.Users
                .Where(u => !u.IsDeleted
                            && u.Username.ToLower().Equals(userName.ToLower()))
                .FirstOrDefaultAsync(cancellationToken);
        }

    }
}
