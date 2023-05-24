using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Arrival = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Departure = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DockId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ship_Dock_DockId",
                        column: x => x.DockId,
                        principalTable: "Dock",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tugboat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tugboat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tugboat_Ship_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ship",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ship_DockId",
                table: "Ship",
                column: "DockId");

            migrationBuilder.CreateIndex(
                name: "IX_Tugboat_ShipId",
                table: "Tugboat",
                column: "ShipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tugboat");

            migrationBuilder.DropTable(
                name: "Ship");

            migrationBuilder.DropTable(
                name: "Dock");
        }
    }
}
