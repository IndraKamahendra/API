using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class EU_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_Education_Id",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "Education_Id",
                table: "Profilings");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Profilings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "Educations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UniversityId",
                table: "Educations",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Universities_UniversityId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UniversityId",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Profilings");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Educations");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.AddColumn<int>(
                name: "Education_Id",
                table: "Profilings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_Education_Id",
                table: "Profilings",
                column: "Education_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Education_Education_Id",
                table: "Profilings",
                column: "Education_Id",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
