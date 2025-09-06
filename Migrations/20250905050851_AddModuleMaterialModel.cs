using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LmsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddModuleMaterialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module_materials",
                columns: table => new
                {
                    module_id = table.Column<int>(type: "integer", nullable: false),
                    material_id = table.Column<int>(type: "integer", nullable: false),
                    material_id1 = table.Column<int>(type: "integer", nullable: false),
                    course_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_module_materials", x => new { x.module_id, x.material_id });
                    table.ForeignKey(
                        name: "fk_module_materials_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_module_materials_materials_material_id1",
                        column: x => x.material_id1,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_module_materials_modules_material_id",
                        column: x => x.material_id,
                        principalTable: "modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_module_materials_course_id",
                table: "module_materials",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_module_materials_material_id",
                table: "module_materials",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "ix_module_materials_material_id1",
                table: "module_materials",
                column: "material_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "module_materials");
        }
    }
}
