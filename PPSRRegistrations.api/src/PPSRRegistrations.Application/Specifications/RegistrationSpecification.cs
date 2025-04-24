using PPSRRegistrations.Application.ViewModels;
using PPSRRegistrations.Domain.Models;
using PPSRRegistrations.Domain.Specification.Interface;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PPSRRegistrations.Application.Specifications
{
    internal class RegistrationSpecification : ISpecificationConfiguration<CsvRecordViewModel>
    {
        public ISpecification<CsvRecordViewModel> Map(ISpecification<CsvRecordViewModel> builder)
        {
            builder.SetStopOnFirstFailure(true);

            builder.IsSatisfiedBy(x => !string.IsNullOrWhiteSpace(x.GrantorFirstName) && x.GrantorFirstName.Length <= 35, 
                "Grantor First Name is required and must be <= 35 characters.", 412);

            builder.IsSatisfiedBy(x => string.IsNullOrWhiteSpace(x.GrantorMiddleNames) || x.GrantorMiddleNames.Length <= 75, 
                "Grantor Middle Names must be <= 75 characters.", 412);

            builder.IsSatisfiedBy(x => !string.IsNullOrWhiteSpace(x.GrantorLastName) && x.GrantorLastName.Length <= 35, 
                "Grantor Last Name is required and must be <= 35 characters.", 412);

            builder.IsSatisfiedBy(x => !string.IsNullOrWhiteSpace(x.VIN) && x.VIN.Length == 17, 
                "VIN is required and must be exactly 17 characters.", 412);

            builder.IsSatisfiedBy(x => DateOnly.TryParseExact(x.RegistrationStartDateRaw, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var regDate), 
                "Registration start date is required and must be a valid date.", 412);

            builder.IsSatisfiedBy(x => !string.IsNullOrWhiteSpace(x.RegistrationDuration) &&
                new[] { "7", "25", "N/A" }.Contains(x.RegistrationDuration), 
                "Registration duration must be '7 years', '25 years', or 'N/A'.", 412);

            builder.IsSatisfiedBy(x => !string.IsNullOrWhiteSpace(x.SPGACN.Replace(" ", "")) && Regex.IsMatch(x.SPGACN.Replace(" ", ""), @"^\d{9}$"), 
                "SPG ACN is required and must be a 9-digit number.", 412);

            builder.IsSatisfiedBy(x => !string.IsNullOrWhiteSpace(x.SPGOrganizationName) && x.SPGOrganizationName.Length <= 75, 
                "SPG Organization Name is required and must be <= 75 characters.", 412);

            return builder;
        }
    }
}
