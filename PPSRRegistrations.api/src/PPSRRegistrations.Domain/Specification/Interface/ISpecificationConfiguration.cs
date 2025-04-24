namespace PPSRRegistrations.Domain.Specification.Interface
{
    public interface ISpecificationConfiguration<T> where T : class
    {
        ISpecification<T> Map(ISpecification<T> builder);
    }
}
