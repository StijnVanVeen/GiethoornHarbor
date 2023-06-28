using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedNewStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipId",
                table: "Tugboat");

            migrationBuilder.AddColumn<int>(
                name: "ArrivalId",
                table: "Tugboat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "Tugboat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipId",
                table: "Dock",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Arrival",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    DockId = table.Column<int>(type: "int", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrival", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    DockId = table.Column<int>(type: "int", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeftHarbor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departure", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arrival");

            migrationBuilder.DropTable(
                name: "Departure");

            migrationBuilder.DropColumn(
                name: "ArrivalId",
                table: "Tugboat");

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "Tugboat");

            migrationBuilder.DropColumn(
                name: "ShipId",
                table: "Dock");

            migrationBuilder.AddColumn<int>(
                name: "ShipId",
                table: "Tugboat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
