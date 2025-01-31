using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherDashboard.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWeatherDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Snow",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaterTemp",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "Snow",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "WaterTemp",
                table: "WeatherRecords");
        }
    }
}
