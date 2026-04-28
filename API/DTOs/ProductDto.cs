public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string? Type { get; set; }
    public string? Brand { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }

    public int TypeId { get; set; }
    public int BrandId { get; set; }
    public int MakeId { get; set; }
    public int ModelId { get; set; }

    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUrl { get; set; }

    public int Year { get; set; }
    public string FuelType { get; set; }
    public string TransmissionType { get; set; }
    public int Kilometers { get; set; }
    public int OwnerNumber { get; set; }
    public string Location { get; set; }
    public DateTime PostingDate { get; set; }
}