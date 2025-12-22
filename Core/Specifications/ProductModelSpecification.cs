using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductModelSpecification : BaseSpecification<ProductModel>
{
    public ProductModelSpecification(int makeId)
        : base(x => x.MakeId == makeId) // Filter models by MakeId
    {
        AddOrderBy(x => x.ModelName); // Optional: Order by ModelName
    }
}
