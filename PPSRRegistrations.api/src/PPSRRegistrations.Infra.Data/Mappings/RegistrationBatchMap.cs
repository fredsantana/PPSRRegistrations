using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPSRRegistrations.Domain.Models;

namespace PPSRRegistrations.Infra.Data.Mappings
{
    internal class RegistrationBatchMap : Mapping<RegistrationBatch>
    {
        public override void Map(EntityTypeBuilder<RegistrationBatch> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FileName)
                  .IsRequired()
                  .HasMaxLength(255);
        }
    }
}
