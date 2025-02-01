using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherDashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddedCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "WeatherRecords");
        }
    }
}
