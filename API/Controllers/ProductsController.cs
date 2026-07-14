using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

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

        var spec = new ProductSpecification(specParams);

        return await CreatePagedResult(repo, spec, specParams.PageIndex, specParams.PageSize);
    }

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

    // [HttpGet("{id:int}")] // api/products/2
    // public async Task<ActionResult<Product>> GetProduct(int id)
    // {
    //     var product = await repo.GetByIdAsync(id);

    //     if (product == null) return NotFound();

    //     return product;
    // }

    [HttpGet("{id:int}")]
public async Task<ActionResult<ProductDto>> GetProduct(int id)
{
    var spec = new ProductDetailsSpecification(id); // 👈 create this
    var product = await repo.GetEntityWithSpec(spec);

    if (product == null) return NotFound();

    var result = new ProductDto
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        PictureUrl = product.PictureUrl,

        Type = product.ProductType?.TypeName,
        Brand = product.ProductBrand?.BrandName,
        Make = product.ProductMake?.MakeName,
        Model = product.ProductModel?.ModelName,

        TypeId = product.TypeId,
        BrandId = product.BrandId,
        MakeId = product.MakeId,
        ModelId = product.ModelId,

        Year = product.Year ?? 0,
        FuelType = product.FuelType,
        TransmissionType = product.TransmissionType,
        Kilometers = product.kilometers ?? 0,
        OwnerNumber = product.OwnerNumber,
        Location = product.Location,
        PostingDate = product.PostingDate
    };

    return Ok(result);
}

    [HttpPost("create-with-images")]
[RequestSizeLimit(10_000_000)]
[RequestFormLimits(MultipartBodyLengthLimit = 10_000_000)]
public async Task<ActionResult> CreateProductWithImages(
    [FromForm] CreateProductDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    const long targetImageSizeBytes = 500 * 1024; // target size after compression
    const int maxImageDimension = 1600;

    if (dto.Images is null || dto.Images.Count == 0)
        return BadRequest("At least one image is required.");

    var uploadsRoot = Path.Combine(
        Directory.GetCurrentDirectory(),
        "wwwroot", "images", "products"
    );

    Directory.CreateDirectory(uploadsRoot);

    var imagePaths = new List<string>();

    foreach (var image in dto.Images.Where(i => i is { Length: > 0 }).Take(4))
    {
        var fileName =
            $"{dto.UserId}_{Guid.NewGuid()}.jpg";

        var filePath = Path.Combine(uploadsRoot, fileName);

        await SaveCompressedImageAsync(image, filePath, targetImageSizeBytes, maxImageDimension);

        var imageUrl = $"{Request.Scheme}://{Request.Host}/images/products/{fileName}";
        imagePaths.Add(imageUrl);
    }

    var product = new Product
    {
        Name = dto.Name,
        Title = dto.Title,
        Description = dto.Description,
        Price = dto.Price,
        Year = dto.Year,
        Color = dto.Color,
        kilometers = dto.kilometers,
        OwnerNumber = dto.OwnerNumber,
        FuelType = dto.FuelType,
        TransmissionType = dto.TransmissionType,
        Location = dto.Location,
        PostingDate = DateTime.UtcNow,
        UserId = dto.UserId,

        TypeId = dto.TypeId,
        BrandId = dto.BrandId,
        MakeId = dto.MakeId,
        ModelId = dto.ModelId,

        PictureUrl = imagePaths.FirstOrDefault(),

        ImagePathOne = imagePaths.ElementAtOrDefault(0),
        ImagePathTwo = imagePaths.ElementAtOrDefault(1),
        ImagePathThree = imagePaths.ElementAtOrDefault(2),
        ImagePathFour = imagePaths.ElementAtOrDefault(3)
    };

    repo.Add(product);

    if (await repo.SaveAllAsync())
        return Ok(product);

    return BadRequest("Failed to create product");
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

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }

    private static async Task SaveCompressedImageAsync(IFormFile image, string filePath, long targetSizeBytes, int maxDimension)
    {
        await using var inputStream = image.OpenReadStream();
        using var sourceImage = await Image.LoadAsync(inputStream);

        var width = sourceImage.Width;
        var height = sourceImage.Height;
        var scale = Math.Min(1d, maxDimension / (double)Math.Max(width, height));
        var newWidth = Math.Max(1, (int)Math.Round(width * scale));
        var newHeight = Math.Max(1, (int)Math.Round(height * scale));

        sourceImage.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(newWidth, newHeight)
        }));

        var quality = 85;
        await SaveImageAsync(sourceImage, filePath, quality);

        while (new FileInfo(filePath).Length > targetSizeBytes && quality > 20)
        {
            quality -= 10;
            await SaveImageAsync(sourceImage, filePath, quality);
        }
    }

    private static async Task SaveImageAsync(Image sourceImage, string filePath, int quality)
    {
        var encoder = new JpegEncoder { Quality = quality };

        await using var outputStream = new FileStream(filePath, FileMode.Create);
        await sourceImage.SaveAsJpegAsync(outputStream, encoder);
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