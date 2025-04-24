namespace PPSRRegistrations.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();

        Task CommitAsync();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
