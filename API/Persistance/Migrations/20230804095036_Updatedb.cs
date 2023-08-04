using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class Updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentDensity",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CurrentHumidity",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CurrentPosition_X",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CurrentPosition_Y",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "CurrentTemperature",
                table: "Shipments");

            migrationBuilder.CreateTable(
                name: "ShipmentHighlights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentPosition_X = table.Column<string>(type: "text", nullable: false),
                    CurrentPosition_Y = table.Column<string>(type: "text", nullable: false),
                    CurrentHumidity = table.Column<string>(type: "text", nullable: false),
                    CurrentTemperature = table.Column<string>(type: "text", nullable: false),
                    CurrentDensity = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ShimpentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentHighlights", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentHighlights");

            migrationBuilder.AddColumn<string>(
                name: "CurrentDensity",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentHumidity",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentPosition_X",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentPosition_Y",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentTemperature",
                table: "Shipments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
