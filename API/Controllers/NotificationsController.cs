using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly StoreContext _context;

    public NotificationsController(StoreContext context)
    {
        _context = context;
    }

    [HttpPost("create-buyer-notifications")]
    public async Task<IActionResult> Create(CreateBuyerNotificationDto dto)
    {
        var notification = new BuyerNotification
        {
            UserId=dto.UserId,
            Email = dto.Email,
            TypeId = dto.TypeId,
            BrandId = dto.BrandId,
            MakeId = dto.MakeId,
            ModelId = dto.ModelId,
            MinKilometers = dto.MinKilometers,
            MaxKilometers = dto.MaxKilometers,
            MinYear = dto.MinYear,
            MaxYear = dto.MaxYear,
            OwnerNumber = dto.OwnerNumber,
            TransmissionType = dto.TransmissionType,
            FuelType = dto.FuelType,
            Location = dto.Location,
            MinPrice = dto.MinPrice,
            MaxPrice = dto.MaxPrice
        };

        _context.BuyerNotifications.Add(notification);
        await _context.SaveChangesAsync();

        // 🔔 Dummy email simulation
        Console.WriteLine($"Notify email sent to {dto.Email}");

        return Ok();
    }
}
