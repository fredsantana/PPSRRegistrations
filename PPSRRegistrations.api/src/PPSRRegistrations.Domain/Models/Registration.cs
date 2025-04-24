namespace PPSRRegistrations.Domain.Models
{
    public class Registration
    {
        public Guid Id { get; set; }

        public Guid RegistrationBatchId { get; set; }

        public string GrantorFirstName { get; set; }
        public string? GrantorMiddleNames { get; set; }
        public string GrantorLastName { get; set; }

        public string VIN { get; set; }

        public DateOnly RegistrationStartDate { get; set; }

        public string RegistrationDuration { get; set; }

        public string SPGACN { get; set; }
        public string SPGOrganizationName { get; set; }
    }
}
