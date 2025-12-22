using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type)) &&
        (!specParams.TypeId.HasValue || x.TypeId == specParams.TypeId) &&
        (!specParams.BrandId.HasValue || x.BrandId == specParams.BrandId) &&
        (!specParams.MakeId.HasValue || x.MakeId == specParams.MakeId) &&
        (!specParams.ModelId.HasValue || x.ModelId == specParams.ModelId) &&
         (!specParams.MinKilometers.HasValue || x.kilometers >= specParams.MinKilometers) &&
        (!specParams.MaxKilometers.HasValue || x.kilometers <= specParams.MaxKilometers) &&
        (!specParams.MinYear.HasValue || x.Year >= specParams.MinYear) &&
        (!specParams.MaxYear.HasValue || x.Year <= specParams.MaxYear) &&
        (!specParams.OwnerNumber.HasValue || x.OwnerNumber == specParams.OwnerNumber) &&
        (!specParams.MinPrice.HasValue || x.Price >= specParams.MinPrice) &&
        (!specParams.MaxPrice.HasValue || x.Price <= specParams.MaxPrice) &&
        (string.IsNullOrEmpty(specParams.TransmissionType) || x.TransmissionType == specParams.TransmissionType.Trim())

    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex -1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}