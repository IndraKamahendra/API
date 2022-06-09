using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fix_profilingg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings");

            migrationBuilder.AlterColumn<string>(
                name: "EducationId",
                table: "Profilings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationId1",
                table: "Profilings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_EducationId1",
                table: "Profilings",
                column: "EducationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_EducationId1",
                table: "Profilings",
                column: "EducationId1",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profilings_Educations_EducationId1",
                table: "Profilings");

            migrationBuilder.DropIndex(
                name: "IX_Profilings_EducationId1",
                table: "Profilings");

            migrationBuilder.DropColumn(
                name: "EducationId1",
                table: "Profilings");

            migrationBuilder.AlterColumn<int>(
                name: "EducationId",
                table: "Profilings",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profilings_EducationId",
                table: "Profilings",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profilings_Educations_EducationId",
                table: "Profilings",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
