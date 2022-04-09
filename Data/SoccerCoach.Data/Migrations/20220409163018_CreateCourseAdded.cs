using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerCoach.Data.Migrations
{
    public partial class CreateCourseAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Courses_CourseId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CourseId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Clients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CourseId",
                table: "Clients",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Courses_CourseId",
                table: "Clients",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
