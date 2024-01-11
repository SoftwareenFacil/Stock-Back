using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Back.DAL.Migrations
{
    /// <inheritdoc />
    public partial class vigencyattribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxID",
                table: "client",
                newName: "Rut");

            migrationBuilder.AddColumn<bool>(
                name: "Vigency",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "client",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Vigency",
                table: "client",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated", "Vigency" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 52, 30, 16, DateTimeKind.Utc).AddTicks(9464), "q8StP6cPfMLCCr4O8iKMcKU6K2U2E6j3WJsjO4JjPLUNeBk2", new DateTime(2024, 1, 11, 14, 52, 30, 16, DateTimeKind.Utc).AddTicks(9465), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vigency",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "client");

            migrationBuilder.DropColumn(
                name: "Vigency",
                table: "client");

            migrationBuilder.RenameColumn(
                name: "Rut",
                table: "client",
                newName: "TaxID");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 6, 19, 33, 17, 812, DateTimeKind.Utc).AddTicks(4505), "WP5OdgVxC39eESynFAvwi0BwheNyhXbVtkB32nfhn3MUyCUj", new DateTime(2024, 1, 6, 19, 33, 17, 812, DateTimeKind.Utc).AddTicks(4506) });
        }
    }
}
