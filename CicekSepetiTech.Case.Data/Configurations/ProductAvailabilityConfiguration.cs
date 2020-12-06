using CicekSepetiTech.Case.Data.DbEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CicekSepetiTech.Case.Data.Configurations
{
    public class ProductAvailabilityConfiguration : IEntityTypeConfiguration<ProductAvailability>
    {
        public void Configure(EntityTypeBuilder<ProductAvailability> builder)
        {
            builder.ToTable(nameof(ProductAvailability));
            builder.HasKey(x => x.Id);
        }
    }
}