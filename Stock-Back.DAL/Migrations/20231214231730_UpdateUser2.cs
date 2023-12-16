using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Back.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
