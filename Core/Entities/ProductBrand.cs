using System;

namespace Core.Entities;

public class ProductBrand: BaseEntity
{
    public string BrandName { get; set; } = string.Empty;
    
    // Foreign Key for Type
    public int TypeId { get; set; }
    public ProductType? ProductType { get; set; } // Navigation property

    // Navigation property for related Makes
    public ICollection<ProductMake> Makes { get; set; } = new List<ProductMake>();
}