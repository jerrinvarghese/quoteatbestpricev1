using System;

namespace Core.Entities;

public class ProductMake: BaseEntity
{
    public string MakeName { get; set; } = string.Empty;

    // Foreign Key for Brand
    public int BrandId { get; set; }
    public ProductBrand? ProductBrand { get; set; } // Navigation property

    // Navigation property for related Models
    public ICollection<ProductModel> ProductModels { get; set; } = new List<ProductModel>();
}