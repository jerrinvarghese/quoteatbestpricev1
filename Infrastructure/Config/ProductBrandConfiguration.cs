using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class BrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.BrandName).IsRequired().HasMaxLength(100);
        builder.HasMany(b => b.Makes).WithOne(m => m.ProductBrand).HasForeignKey(m => m.BrandId);
    }
}
