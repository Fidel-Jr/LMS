using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModuleMaterialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_module_materials_courses_course_id",
                table: "module_materials");

            migrationBuilder.DropForeignKey(
                name: "fk_module_materials_materials_material_id1",
                table: "module_materials");

            migrationBuilder.DropForeignKey(
                name: "fk_module_materials_modules_material_id",
                table: "module_materials");

            migrationBuilder.DropIndex(
                name: "ix_module_materials_course_id",
                table: "module_materials");

            migrationBuilder.DropIndex(
                name: "ix_module_materials_material_id1",
                table: "module_materials");

            migrationBuilder.DropColumn(
                name: "course_id",
                table: "module_materials");

            migrationBuilder.DropColumn(
                name: "material_id1",
                table: "module_materials");

            migrationBuilder.AddForeignKey(
                name: "fk_module_materials_materials_material_id",
                table: "module_materials",
                column: "material_id",
                principalTable: "materials",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_module_materials_modules_module_id",
                table: "module_materials",
                column: "module_id",
                principalTable: "modules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_module_materials_materials_material_id",
                table: "module_materials");

            migrationBuilder.DropForeignKey(
                name: "fk_module_materials_modules_module_id",
                table: "module_materials");

            migrationBuilder.AddColumn<int>(
                name: "course_id",
                table: "module_materials",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "material_id1",
                table: "module_materials",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_module_materials_course_id",
                table: "module_materials",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_module_materials_material_id1",
                table: "module_materials",
                column: "material_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_module_materials_courses_course_id",
                table: "module_materials",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_module_materials_materials_material_id1",
                table: "module_materials",
                column: "material_id1",
                principalTable: "materials",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_module_materials_modules_material_id",
                table: "module_materials",
                column: "material_id",
                principalTable: "modules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
