using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Data.Migrations
{
    public partial class Addedappointmentdatefordoctorandmadedatefieldsrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextFreeAppointmentDate",
                table: "Doctors",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextFreeAppointmentDate",
                table: "Doctors");
        }
    }
}
