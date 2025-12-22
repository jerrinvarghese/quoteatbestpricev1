using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductBrandSpecification : BaseSpecification<ProductBrand>
{
    public ProductBrandSpecification(int typeId)
        : base(x => x.TypeId == typeId) // Filter brands by TypeId
    {
        AddOrderBy(x => x.BrandName); // Optional: Order by BrandName
    }
}
