using CsvHelper.Configuration.Attributes;

namespace PPSRRegistrations.Application.ViewModels
{
    public class CsvRecordViewModel
    {
        [Name("Grantor First Name")]
        public string GrantorFirstName { get; set; }

        [Name("Grantor Middle Names")]
        public string GrantorMiddleNames { get; set; }

        [Name("Grantor Last Name")]
        public string GrantorLastName { get; set; }

        [Name("VIN")]
        public string VIN { get; set; }

        [Name("Registration start date")]
        public string RegistrationStartDateRaw { get; set; }

        [Name("Registration duration")]
        public string RegistrationDuration { get; set; }

        [Name("SPG ACN")]
        public string SPGACN { get; set; }

        [Name("SPG Organization Name")]
        public string SPGOrganizationName { get; set; }
    }
}
