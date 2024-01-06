using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Back.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SuperAdmin",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuperAdmin",
                table: "users");
        }
    }
}
