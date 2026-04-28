using System;
using Core.Entities;

namespace Core.Specifications;
public class ProductDetailsSpecification : BaseSpecification<Product>
{
    public ProductDetailsSpecification(int id) : base(x => x.Id == id)
    {
        //This specification is used to get the TypeName, BrandName, MakeName, and 
        // ModelName of a single product by product id as Products table have only 
        //TypeId, BrandId, MakeId, and ModelId as foreign keys. 
        // So we need to include the related tables to get the names of the 
        // Type, Brand, Make, and Model. This is used in product details page 
        // to show the details of the product.
        AddInclude(x => x.ProductType!);
        AddInclude(x => x.ProductBrand!);
        AddInclude(x => x.ProductMake!);
        AddInclude(x => x.ProductModel!);
    }
}