using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PPSRRegistrations.Domain.Interfaces.UoW;

namespace PPSRRegistrations.Infra.Data.UoW
{
    public class UnitOfWork<ContextDB> : IUnitOfWork where ContextDB : DbContext
    {
        private readonly ContextDB _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ContextDB context)
        {
            _context = context;
            _transaction = null;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            _context.Dispose();
        }
    }
}
