using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddInstituteImageContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InstituteId",
                table: "ImageContents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImageContents_InstituteId",
                table: "ImageContents",
                column: "InstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageContents_Institutes_InstituteId",
                table: "ImageContents",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageContents_Institutes_InstituteId",
                table: "ImageContents");

            migrationBuilder.DropIndex(
                name: "IX_ImageContents_InstituteId",
                table: "ImageContents");

            migrationBuilder.DropColumn(
                name: "InstituteId",
                table: "ImageContents");
        }
    }
}
