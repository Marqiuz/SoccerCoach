using Microsoft.EntityFrameworkCore.Migrations;

namespace SoccerCoach.Data.Migrations
{
    public partial class SeedersAddedAndModelsFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Playstyle",
                table: "Positions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Playstyle",
                table: "Positions");
        }
    }
}
