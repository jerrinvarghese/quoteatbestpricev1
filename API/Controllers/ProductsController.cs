using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> repo, IGenericRepository<ProductType> producttyperepo,
IGenericRepository<ProductBrand> productbrandRepo,
    IGenericRepository<ProductMake> productmakeRepo,
    IGenericRepository<ProductModel> productmodelRepo,
    IGenericRepository<City> cityRepo) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
        [FromQuery]ProductSpecParams specParams)
    {
        //before running the below four await statements,
        //make sure to uncomment below lines in Program.cs
        // builder.Services.AddControllers().AddJsonOptions(x =>
        //    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        //await GetProductTypes();
        //await GetBrandsByType(1);
        //await GetMakesByBrand(1);
        //await GetModelsByMake(1);
        var spec = new ProductSpecification(specParams);

        return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
    }

    // [HttpGet("product-types")]
    // public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    // {
    //     var spec = new ProductTypeSpecification();

    //     var productTypes = await producttyperepo.ListAsync(spec);

    //     return Ok(productTypes);
    // }

    [HttpGet("product-types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        var spec = new ProductTypeSpecification(); // Specification to fetch all active types
        var types = await producttyperepo.ListAsync(spec);

        return Ok(types);
    }

    [HttpGet("brands/{typeId:int}")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrandsByType(int typeId)
    {
        var spec = new ProductBrandSpecification(typeId); // Specification to filter brands by TypeId
        var brands = await productbrandRepo.ListAsync(spec);

        return Ok(brands);
    }

    [HttpGet("makes/{brandId:int}")]
    public async Task<ActionResult<IReadOnlyList<ProductMake>>> GetMakesByBrand(int brandId)
    {
        var spec = new ProductMakeSpecification(brandId); // Specification to filter makes by BrandId
        var makes = await productmakeRepo.ListAsync(spec);

        return Ok(makes);
    }

    [HttpGet("models/{makeId:int}")]
    public async Task<ActionResult<IReadOnlyList<ProductModel>>> GetModelsByMake(int makeId)
    {
        var spec = new ProductModelSpecification(makeId); // Specification to filter models by MakeId
        var models = await productmodelRepo.ListAsync(spec);

        return Ok(models);
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);

        if (await repo.SaveAllAsync())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        return BadRequest("Problem creating product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        repo.Update(product);

        if (await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        repo.Remove(product);

        if (await repo.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();

        return Ok(await repo.ListAsync(spec));
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec = new TypeListSpecification();

        return Ok(await repo.ListAsync(spec));
    }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }
    
   [HttpGet("locations")]
public async Task<ActionResult<IReadOnlyList<string>>> GetLocations(
    [FromQuery] string search)
{
    if (string.IsNullOrWhiteSpace(search) || search.Length < 3)
        return Ok(new List<string>());

    var spec = new CitySpecification(search);

    var cities = await cityRepo.ListAsync(spec);

    var result = cities
        .Select(c => $"{c.Name}, {c.State}")
        .ToList();

    return Ok(result);
}
}