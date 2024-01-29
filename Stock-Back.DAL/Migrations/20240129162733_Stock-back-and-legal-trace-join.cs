using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Back.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Stockbackandlegaltracejoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_client",
                table: "client");

            migrationBuilder.RenameTable(
                name: "client",
                newName: "clients");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Address", "Created", "Name", "Password", "Updated", "Vigency" },
                values: new object[] { "", new DateTime(2024, 1, 29, 16, 27, 32, 934, DateTimeKind.Utc).AddTicks(5182), "admin", "LGT/+IWMZ1MISHgLgEUc38KP7pFxtYmp+zVI65rUrtholXbc", new DateTime(2024, 1, 29, 16, 27, 32, 934, DateTimeKind.Utc).AddTicks(5183), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "users");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "client");

            migrationBuilder.AddPrimaryKey(
                name: "PK_client",
                table: "client",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Name", "Password", "Updated", "Vigency" },
                values: new object[] { new DateTime(2024, 1, 12, 14, 0, 47, 966, DateTimeKind.Utc).AddTicks(4624), "", "edqLdBGePsLt3U5N10FX235bPq+UNd/HWqUZKS4PC8UztDnn", new DateTime(2024, 1, 12, 14, 0, 47, 966, DateTimeKind.Utc).AddTicks(4626), false });
        }
    }
}
