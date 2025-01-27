﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherDashboard.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    Phenomenon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nebulozity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherRecords");
        }
    }
}
