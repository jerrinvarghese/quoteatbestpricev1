using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TypeName).IsRequired().HasMaxLength(100);
        builder.Property(t => t.TypeDescription).HasMaxLength(500);
        builder.HasMany(t => t.ProductBrands).WithOne(b => b.ProductType).HasForeignKey(b => b.TypeId);
    }
}