using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addingUniversityImagesname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ImageContents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ImageContents");
        }
    }
}
