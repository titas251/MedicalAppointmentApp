using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Data.Migrations
{
    public partial class AddedBlackListColumnsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BlackListedEndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlackListed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlackListedEndDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsBlackListed",
                table: "AspNetUsers");
        }
    }
}
