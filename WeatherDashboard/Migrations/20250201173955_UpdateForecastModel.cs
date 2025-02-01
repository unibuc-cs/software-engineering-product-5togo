using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherDashboard.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForecastModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Forecasts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Forecasts");
        }
    }
}
