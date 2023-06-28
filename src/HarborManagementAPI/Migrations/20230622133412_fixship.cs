using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ship_Dock_DockId",
                table: "Ship");

            migrationBuilder.DropForeignKey(
                name: "FK_Tugboat_Ship_ShipId",
                table: "Tugboat");

            migrationBuilder.DropIndex(
                name: "IX_Tugboat_ShipId",
                table: "Tugboat");

            migrationBuilder.DropIndex(
                name: "IX_Ship_DockId",
                table: "Ship");

            migrationBuilder.DropColumn(
                name: "Arrival",
                table: "Ship");

            migrationBuilder.DropColumn(
                name: "Departure",
                table: "Ship");

            migrationBuilder.DropColumn(
                name: "DockId",
                table: "Ship");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Ship",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Ship",
                newName: "Brand");

            migrationBuilder.AlterColumn<int>(
                name: "ShipId",
                table: "Tugboat",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LengthInMeters",
                table: "Ship",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthInMeters",
                table: "Ship");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ship",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Ship",
                newName: "Size");

            migrationBuilder.AlterColumn<int>(
                name: "ShipId",
                table: "Tugboat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Arrival",
                table: "Ship",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Departure",
                table: "Ship",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DockId",
                table: "Ship",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tugboat_ShipId",
                table: "Tugboat",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Ship_DockId",
                table: "Ship",
                column: "DockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ship_Dock_DockId",
                table: "Ship",
                column: "DockId",
                principalTable: "Dock",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tugboat_Ship_ShipId",
                table: "Tugboat",
                column: "ShipId",
                principalTable: "Ship",
                principalColumn: "Id");
        }
    }
}
