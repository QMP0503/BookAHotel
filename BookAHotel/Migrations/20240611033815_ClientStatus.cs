using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAHotel.Migrations
{
    /// <inheritdoc />
    public partial class ClientStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBooking",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Clients",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Clients");

            migrationBuilder.AddColumn<bool>(
                name: "HasBooking",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
