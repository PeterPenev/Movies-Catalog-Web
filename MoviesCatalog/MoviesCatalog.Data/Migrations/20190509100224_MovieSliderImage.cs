using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesCatalog.Data.Migrations
{
    public partial class MovieSliderImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SliderImage",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImage",
                table: "Movies");
        }
    }
}
