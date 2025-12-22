using System;

namespace Core.Entities;

public class ProductModel: BaseEntity
{
    public string ModelName { get; set; } = string.Empty;

    // Foreign Key for Make
    public int MakeId { get; set; }
    public ProductMake? ProductMake { get; set; } // Navigation property
}