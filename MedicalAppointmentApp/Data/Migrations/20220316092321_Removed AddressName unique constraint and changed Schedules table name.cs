using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalAppointmentApp.Data.Migrations
{
    public partial class RemovedAddressNameuniqueconstraintandchangedSchedulestablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Doctors_DoctorId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Institutions_InstitutionId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_Schedule_ScheduleId",
                table: "ScheduleDetails");

            migrationBuilder.DropIndex(
                name: "IX_Institutions_Name_Address",
                table: "Institutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_InstitutionId_DoctorId",
                table: "Schedules",
                newName: "IX_Schedules_InstitutionId_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_DoctorId",
                table: "Schedules",
                newName: "IX_Schedules_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Institutions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_Schedules_ScheduleId",
                table: "ScheduleDetails",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Doctors_DoctorId",
                table: "Schedules",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Institutions_InstitutionId",
                table: "Schedules",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "InstitutionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_Schedules_ScheduleId",
                table: "ScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Doctors_DoctorId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Institutions_InstitutionId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_InstitutionId_DoctorId",
                table: "Schedule",
                newName: "IX_Schedule_InstitutionId_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedule",
                newName: "IX_Schedule_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Institutions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_Name_Address",
                table: "Institutions",
                columns: new[] { "Name", "Address" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Doctors_DoctorId",
                table: "Schedule",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Institutions_InstitutionId",
                table: "Schedule",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "InstitutionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_Schedule_ScheduleId",
                table: "ScheduleDetails",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
