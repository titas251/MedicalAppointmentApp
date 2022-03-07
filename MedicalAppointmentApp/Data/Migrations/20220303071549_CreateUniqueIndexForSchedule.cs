using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalAppointmentApp.Data.Migrations
{
    public partial class CreateUniqueIndexForSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedule_InstitutionId",
                table: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_InstitutionId_DoctorId",
                table: "Schedule",
                columns: new[] { "InstitutionId", "DoctorId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedule_InstitutionId_DoctorId",
                table: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_InstitutionId",
                table: "Schedule",
                column: "InstitutionId");
        }
    }
}
