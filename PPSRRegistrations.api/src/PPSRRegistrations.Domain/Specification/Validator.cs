using PPSRRegistrations.Domain.Specification.Interface;

namespace PPSRRegistrations.Domain.Specification
{
    public static class Validator
    {
        public static ValidatorResult IsValid<T, S>(this T model, bool throwError = true) 
            where T : class
            where S : ISpecificationConfiguration<T>, new()
        {
            /*Type specConfType = typeof(S);
            var specConfObj = Activator.CreateInstance(specConfType) as ISpecificationConfiguration<T>;

            return specConfObj.IsValid(model, throwError);*/

            var specConfObj = new S();

            if (specConfObj != null)
            {
                return specConfObj.IsValid(model, throwError);
            }

            throw new InvalidOperationException("Invalid configuration specification.");
        }

        public static ValidatorResult IsValid<T>(this ISpecificationConfiguration<T>? specConfObj, T model, bool throwError = true) 
            where T : class
        {
            //if (model == null)
            //{
            //    throw new ArgumentNullException(nameof(model));
            //}

            Type specType = typeof(Specification<T>);
            var specObj = Activator.CreateInstance(specType, new object[] { model }) as Specification<T>;

            //if (model != null && specConfObj != null && specObj != null)
            if (specConfObj != null && specObj != null)
                return specConfObj.Map(specObj).Validate(throwError);
            else
                throw new InvalidOperationException("Could not recognize validator object.");
        }
    }
}
