using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModulePropertyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "course_id",
                table: "modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_modules_course_id",
                table: "modules",
                column: "course_id");

            migrationBuilder.AddForeignKey(
                name: "fk_modules_courses_course_id",
                table: "modules",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_modules_courses_course_id",
                table: "modules");

            migrationBuilder.DropIndex(
                name: "ix_modules_course_id",
                table: "modules");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "modules");
        }
    }
}
