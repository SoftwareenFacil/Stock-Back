using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Back.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ruttotaxid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rut",
                table: "client",
                newName: "TaxId");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 12, 14, 0, 47, 966, DateTimeKind.Utc).AddTicks(4624), "edqLdBGePsLt3U5N10FX235bPq+UNd/HWqUZKS4PC8UztDnn", new DateTime(2024, 1, 12, 14, 0, 47, 966, DateTimeKind.Utc).AddTicks(4626) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxId",
                table: "client",
                newName: "Rut");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password", "Updated" },
                values: new object[] { new DateTime(2024, 1, 11, 14, 52, 30, 16, DateTimeKind.Utc).AddTicks(9464), "q8StP6cPfMLCCr4O8iKMcKU6K2U2E6j3WJsjO4JjPLUNeBk2", new DateTime(2024, 1, 11, 14, 52, 30, 16, DateTimeKind.Utc).AddTicks(9465) });
        }
    }
}
