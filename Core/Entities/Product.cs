using System;

namespace Core.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }// remove this if not needed
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUrl { get; set; }
    // public required string Type { get; set; }
    // public required string Brand { get; set; }
    //public int QuantityInStock { get; set; }//remove this if not needed
    public int? Year{ get; set; } // Year of manufacture or model year
    public string? Color { get; set; } // Color of the product
    public int? kilometers { get; set; } // Kilometers driven, applicable for vehicles

    public required string Title { get; set; } // Title or headline for the product listing
    public int OwnerNumber { get; set; } // Owner number, could be used for tracking or identification  

    public string? FuelType{ get; set; } // Type of fuel used (e.g., petrol, diesel, electric)

    public string? TransmissionType { get; set; } // Transmission type (e.g., manual, automatic)

    public required string Location{ get; set; } // Location of the product, useful for local listings

    public DateTime PostingDate { get; set; } // Date when the product was posted
    //Add as foreign key in future
    public int UserId { get; set; } // UserId of the owner or seller.
    public required string ImagePathOne{get; set; } // Path to the first image of the product

    public string? ImagePathTwo{get; set; } // Path to the first image of the product
    public string? ImagePathThree{get; set; } // Path to the first image of the product
    public string? ImagePathFour{get; set; } // Path to the first image of the product
    public string? ImagePathFive{get; set; } // Path to the first image of the product

    //Add as foreign key in future
    public int? PaymentId { get; set; } // PaymentId for the product, could be used for payment processing
    //Add as foreign key in future
    public int? PaymentPlanId { get; set; } // PaymentPlanId for the product, could be used for payment processing

    // Foreign Keys
    public int TypeId { get; set; }
    public int BrandId { get; set; }
    public int MakeId { get; set; }
    public int ModelId { get; set; }

    // Navigation Properties
    public ProductType? ProductType { get; set; }
    public ProductBrand? ProductBrand { get; set; }
    public ProductMake? ProductMake { get; set; }
    public ProductModel? ProductModel { get; set; }
}
