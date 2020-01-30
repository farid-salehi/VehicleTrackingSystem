
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class UserRoleRepository : Repository<UserRoleModel>, IUserRoleRepository
    {
        public UserRoleRepository(VehicleTrackingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<RoleModel>> GetUserRoleListAsync(int userId, CancellationToken cancellationToken)
        {
            return await DbContext.UserRoles
                .Include(t => t.UserInfo)
                .Where(u => !u.IsDeleted
                            && u.UserId == userId)
                .Select(u => u.RoleInfo)
                .ToListAsync(cancellationToken);

        }
    }
}
