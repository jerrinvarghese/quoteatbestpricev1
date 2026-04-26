public class CreateBuyerNotificationDto
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;

    public int? TypeId { get; set; }
    public int? BrandId { get; set; }
    public int? MakeId { get; set; }
    public int? ModelId { get; set; }

    public int? MinKilometers { get; set; }
    public int? MaxKilometers { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }

    public int? OwnerNumber { get; set; }
    public string? TransmissionType { get; set; }
    public string? FuelType { get; set; }
    public string? Location { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
