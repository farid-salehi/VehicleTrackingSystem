using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<bool> AddAsync(T entity, CancellationToken cancellationToken);
        Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);
        bool HardRemove(T entity);
        bool HardRemoveRange(IEnumerable<T> entities);


        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predict, CancellationToken cancellationToken);
    }
}
