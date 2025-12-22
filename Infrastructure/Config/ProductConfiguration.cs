using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Name).IsRequired();
        builder.HasOne(p => p.ProductType)
               .WithMany()
               .HasForeignKey(p => p.TypeId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        builder.HasOne(p => p.ProductBrand)
               .WithMany()
               .HasForeignKey(p => p.BrandId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        builder.HasOne(p => p.ProductMake)
               .WithMany()
               .HasForeignKey(p => p.MakeId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        builder.HasOne(p => p.ProductModel)
               .WithMany()
               .HasForeignKey(p => p.ModelId)
               .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete
    }
}
