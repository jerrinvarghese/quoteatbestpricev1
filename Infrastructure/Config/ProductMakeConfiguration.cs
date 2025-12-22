using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class MakeConfiguration : IEntityTypeConfiguration<ProductMake>
{
    public void Configure(EntityTypeBuilder<ProductMake> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.MakeName).IsRequired().HasMaxLength(100);
        builder.HasMany(m => m.ProductModels).WithOne(md => md.ProductMake).HasForeignKey(md => md.MakeId);
    }
}