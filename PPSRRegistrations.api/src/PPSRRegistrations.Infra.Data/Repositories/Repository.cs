using Microsoft.EntityFrameworkCore;
using PPSRRegistrations.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PPSRRegistrations.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _dbset;

        protected Repository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task Add(IEnumerable<TEntity> entity)
        {
            await _dbset.AddRangeAsync(entity);
        }

        public async Task<TEntity?> GetById(params object[] ids)
        {
            return await _dbset.FindAsync(ids);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbset.AsNoTracking().ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
        }

        public void Update(TEntity current, TEntity updated)
        {
            _context.Entry(current).CurrentValues.SetValues(updated);
        }

        public void Remove(params object[] ids)
        {
            _context.Remove(GetById(ids));
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbset.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
