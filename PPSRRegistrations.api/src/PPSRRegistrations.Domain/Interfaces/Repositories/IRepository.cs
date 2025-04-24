using System.Linq.Expressions;

namespace PPSRRegistrations.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity entity);
        Task Add(IEnumerable<TEntity> entity);
        Task<TEntity?> GetById(params object[] ids);
        Task<IEnumerable<TEntity>> GetAll();
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Update(TEntity current, TEntity updated);
        void Remove(params object[] ids);
        void Remove(TEntity entity);
        void Remove(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();
    }
}
