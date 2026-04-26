using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddBuyerNotifications : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BuyerNotifications",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(nullable: true),
                Email = table.Column<string>(nullable: false),

                TypeId = table.Column<int>(nullable: true),
                BrandId = table.Column<int>(nullable: true),
                MakeId = table.Column<int>(nullable: true),
                ModelId = table.Column<int>(nullable: true),

                MinKilometers = table.Column<int>(nullable: true),
                MaxKilometers = table.Column<int>(nullable: true),
                MinYear = table.Column<int>(nullable: true),
                MaxYear = table.Column<int>(nullable: true),

                OwnerNumber = table.Column<int>(nullable: true),
                TransmissionType = table.Column<string>(nullable: true),
                FuelType = table.Column<string>(nullable: true),
                Location = table.Column<string>(nullable: true),

                MinPrice = table.Column<decimal>(nullable: true),
                MaxPrice = table.Column<decimal>(nullable: true),

                CreatedAt = table.Column<DateTime>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BuyerNotifications", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "BuyerNotifications");
    }
}
