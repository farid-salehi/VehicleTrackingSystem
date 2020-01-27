
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations
{
    public class Repository<T> : IRepository<T> where T :BaseModel 
    {
        protected readonly VehicleTrackingDbContext DbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(VehicleTrackingDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity,cancellationToken);
            return true;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities,cancellationToken);
            return true;
        }

        public bool RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    if (entity.IsDeleted)
                    {
                        continue;
                    }
                    entity.IsDeleted = true;
                    entity.UpdatedDateTime = DateTime.Now;
                    _dbSet.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }
                return true;
            }
            catch (Exception)
            {
                //logger
                return false;
            }
        }

        public bool HardRemove(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public bool HardRemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x => !x.IsDeleted).ToListAsync(cancellationToken);

        }
        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            var obj = await _dbSet.FindAsync(new object[]{id},cancellationToken);
            if (obj == null || obj.IsDeleted)
            {
                return null;
            }
            return obj;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predict, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predict).ToListAsync(cancellationToken);
        }

        public bool Remove(T entity)
        {
            try
            {
                if (entity.IsDeleted)
                {
                    return true;
                }
                entity.IsDeleted = true;
                entity.UpdatedDateTime = DateTime.Now;
                _dbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                //logger
                return false;
            }
        }
    }
}
