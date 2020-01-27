
using System.Collections.Generic;
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

        public async Task<ICollection<UserModel>> GetUserListAsync
            (int limit, int offset, bool orderByDescending, CancellationToken cancellationToken)
        {
            if (orderByDescending)
            {
                return await DbContext.Users
                    .Where(v => !v.IsDeleted)
                    .OrderByDescending(v => v.CreatedDateTime)
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync(cancellationToken);
            }
            return await DbContext.Users
                .Where(v => !v.IsDeleted)
                .OrderBy(v => v.CreatedDateTime)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);

        }

    }
}
