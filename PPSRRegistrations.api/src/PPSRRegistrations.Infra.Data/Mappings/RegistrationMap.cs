using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPSRRegistrations.Domain.Models;

namespace PPSRRegistrations.Infra.Data.Mappings
{
    internal class RegistrationMap : Mapping<Registration>
    {
        public override void Map(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.RegistrationBatchId)
                  .IsRequired();
                  
            builder.Property(e => e.GrantorFirstName)
                  .IsRequired()
                  .HasMaxLength(35);

            builder.Property(e => e.GrantorMiddleNames)
                  .HasMaxLength(75);

            builder.Property(e => e.GrantorLastName)
                  .IsRequired()
                  .HasMaxLength(35);

            builder.Property(e => e.VIN)
                  .IsRequired()
                  .HasMaxLength(17);

            builder.HasIndex(e => e.VIN)
                  .IsUnique();

            builder.Property(e => e.RegistrationStartDate)
                  .IsRequired();

            builder.Property(e => e.RegistrationDuration)
                  .IsRequired();

            builder.Property(e => e.SPGACN)
                  .IsRequired()
                  .HasMaxLength(9);

            builder.HasIndex(e => e.SPGACN)
                  .IsUnique();

            builder.Property(e => e.SPGOrganizationName)
                  .IsRequired()
                  .HasMaxLength(75);
        }
    }
}
