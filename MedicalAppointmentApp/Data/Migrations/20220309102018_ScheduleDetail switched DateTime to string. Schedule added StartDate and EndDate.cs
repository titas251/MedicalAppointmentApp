using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalAppointmentApp.Data.Migrations
{
    public partial class ScheduleDetailswitchedDateTimetostringScheduleaddedStartDateandEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartDateTime",
                table: "ScheduleDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "EndDateTime",
                table: "ScheduleDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Schedule",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Schedule");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "ScheduleDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "ScheduleDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
