using System;

namespace Core.Entities;

public class ProductType: BaseEntity
{
    //public int Id { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string TypeDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<ProductBrand> ProductBrands { get; set; } = new List<ProductBrand>();
}
