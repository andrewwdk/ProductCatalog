using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Entities;

namespace ProductCatalog.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(m => m.ProductId);

            builder.Property(m => m.ProductId)
                   .ValueGeneratedOnAdd();

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(m => m.Description)
                   .IsRequired(false)
                   .HasMaxLength(500);

            builder.Property(m => m.Price)
                   .IsRequired()
                   .HasPrecision(18, 2);

            builder.Property(m => m.CreatedDate)
                   .IsRequired()
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasIndex(m => m.Price);
        }
    }
}
