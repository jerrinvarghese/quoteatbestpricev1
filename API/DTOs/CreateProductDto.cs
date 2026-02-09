public class CreateProductDto
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }
    public int Year { get; set; }

    public string? Color { get; set; }
    public int? kilometers { get; set; }

    public int OwnerNumber { get; set; }
    public string FuelType { get; set; }
    public string TransmissionType { get; set; }
    public string Location { get; set; }

    public int TypeId { get; set; }
    public int BrandId { get; set; }
    public int MakeId { get; set; }
    public int ModelId { get; set; }

    public int UserId { get; set; }

    public List<IFormFile> Images { get; set; } = new();
}
