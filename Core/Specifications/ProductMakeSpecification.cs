using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductMakeSpecification : BaseSpecification<ProductMake>
{
    public ProductMakeSpecification(int brandId)
        : base(x => x.BrandId == brandId) // Filter makes by BrandId
    {
        AddOrderBy(x => x.MakeName); // Optional: Order by MakeName
    }
}
