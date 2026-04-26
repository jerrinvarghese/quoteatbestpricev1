namespace Core.Entities
{
    public class BuyerNotification : BaseEntity
    {
        public int? UserId { get; set; }   // nullable for guest
        public string Email { get; set; } = string.Empty;

        // Filter criteria
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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
