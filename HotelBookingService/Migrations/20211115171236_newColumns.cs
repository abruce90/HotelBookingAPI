using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingLibrary.Migrations
{
    public partial class newColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservationName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationName",
                table: "Bookings");
        }
    }
}
