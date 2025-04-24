using PPSRRegistrations.Domain.Interfaces.UoW;

namespace PPSRRegistrations.Application
{
    public class ApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task Commit()
        {
            await _unitOfWork.CommitAsync();
        }

        public void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _unitOfWork.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _unitOfWork.RollbackTransaction();
        }
    }
}
