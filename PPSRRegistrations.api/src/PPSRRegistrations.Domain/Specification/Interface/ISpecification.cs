namespace PPSRRegistrations.Domain.Specification.Interface
{
    public interface ISpecification<T>
    {
        void IsSatisfiedBy(Func<T, bool> rule, string messageError, int? code = null);

        void IsSatisfiedBy(Func<T, bool> rule, Func<T, string> projection, int? code = null);

        ValidatorResult Validate(bool throwError = false);

        void SetStopOnFirstFailure(bool stopOnFirstFailure);
    }
}
