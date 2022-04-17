using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addingUniversityImagesId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UniversityId",
                table: "ImageContents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ImageContents_UniversityId",
                table: "ImageContents",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageContents_Universities_UniversityId",
                table: "ImageContents",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageContents_Universities_UniversityId",
                table: "ImageContents");

            migrationBuilder.DropIndex(
                name: "IX_ImageContents_UniversityId",
                table: "ImageContents");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "ImageContents");
        }
    }
}
