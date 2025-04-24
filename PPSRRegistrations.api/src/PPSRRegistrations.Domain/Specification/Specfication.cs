using PPSRRegistrations.Domain.Exceptions;
using PPSRRegistrations.Domain.Specification.Interface;

namespace PPSRRegistrations.Domain.Specification
{
    public class Specification<T> : ISpecification<T> where T : class
    {
        private ValidatorResult _result { get; }
        private T _model { get; }

        private bool stopOnFirstFailure = false;

        public Specification(T model)
        {
            _result = new ValidatorResult();
            _model = model;
        }

        public ValidatorResult Validate(bool throwError = true)
        {
            if (throwError && !_result)
                throw new BusinessException(_result, _result);
            return _result;
        }

        public void IsSatisfiedBy(Func<T, bool> rule, string messageError, int? code = null)
        {
            if (!_result && stopOnFirstFailure)
                return;

            if(!rule(_model))
                _result.AddError(messageError, code);
        }
        
        public void IsSatisfiedBy(Func<T, bool> rule, Func<T, string> projection, int? code = null)
        {
            if (!_result && stopOnFirstFailure)
                return;

            if(!rule(_model))
                _result.AddError(projection(_model), code);
        }

        public void SetStopOnFirstFailure(bool stopOnFirstFailure)
        {
            this.stopOnFirstFailure = stopOnFirstFailure;
        }
    }
}
