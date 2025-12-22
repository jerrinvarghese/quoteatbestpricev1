using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductTypeSpecification : BaseSpecification<ProductType>
{
    public ProductTypeSpecification()
        : base(x => x.IsActive) // Filter where IsActive is true
    {
        AddOrderBy(x => x.TypeName); // Optional: Order by TypeName
    }
}
