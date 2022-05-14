using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialUserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserDetails",
                table: "LoginModels",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "LoginModels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDetails",
                table: "LoginModels");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "LoginModels");
        }
    }
}
