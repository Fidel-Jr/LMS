using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LmsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentTeacherModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_students", x => x.id);
                    table.ForeignKey(
                        name: "fk_students_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teachers", x => x.id);
                    table.ForeignKey(
                        name: "fk_teachers_application_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id", "user_id" },
                values: new object[,]
                {
                    { 1, "180aae21-8b04-4495-9927-67c83639fcc9" },
                    { 2, "a928b094-3f85-45bc-9bb8-96aa99f92f88" },
                    { 3, "180aae21-8b04-4495-9927-67c83639fcc9" }
                });

            migrationBuilder.InsertData(
                table: "teachers",
                columns: new[] { "id", "description", "user_id" },
                values: new object[,]
                {
                    { 1, "English Teacher", "75cf164f-e9e0-4772-9b06-f87a31405c57" },
                    { 2, "Physics Teacher", "3c3fa81f-aa22-4177-a033-f8731a6de578" },
                    { 3, "IT Staff", "7500d3ec-929a-48dd-994f-73526a042e62" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_students_user_id",
                table: "students",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_teachers_user_id",
                table: "teachers",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "teachers");
        }
    }
}
