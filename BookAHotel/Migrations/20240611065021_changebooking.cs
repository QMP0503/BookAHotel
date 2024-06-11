using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAHotel.Migrations
{
    /// <inheritdoc />
    public partial class changebooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Rooms");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Bookings",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
