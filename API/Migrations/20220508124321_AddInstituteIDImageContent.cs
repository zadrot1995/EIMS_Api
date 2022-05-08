using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddInstituteIDImageContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.DropForeignKey(
                name: "FK_ImageContents_Institutes_InstituteId",
                table: "ImageContents");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstituteId",
                table: "ImageContents",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<Guid>(
                name: "InstituteId",
                table: "ImageContents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageContents_Institutes_InstituteId",
                table: "ImageContents",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
