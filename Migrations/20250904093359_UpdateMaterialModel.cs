using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMaterialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "materials",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                table: "materials");
        }
    }
}
