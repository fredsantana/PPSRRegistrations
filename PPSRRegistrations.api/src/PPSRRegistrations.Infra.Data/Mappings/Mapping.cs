using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PPSRRegistrations.Infra.Data.Extensions;

namespace PPSRRegistrations.Infra.Data.Mappings
{
    public class Mapping<T> : EntityTypeConfiguration<T> where T : class
    {
        public override void Map(EntityTypeBuilder<T> builder)
        {
        }
    }
}
