using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalAppointmentApp.Data.Migrations
{
    public partial class CreateDoctorInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    InstitutionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.InstitutionId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalSpecialities",
                columns: table => new
                {
                    MedicalSpecialityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSpecialities", x => x.MedicalSpecialityId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    MedicalSpecialityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_MedicalSpecialities_MedicalSpecialityId",
                        column: x => x.MedicalSpecialityId,
                        principalTable: "MedicalSpecialities",
                        principalColumn: "MedicalSpecialityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionDoctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false),
                    InstitutionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionDoctors", x => new { x.DoctorId, x.InstitutionId });
                    table.ForeignKey(
                        name: "FK_InstitutionDoctors_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstitutionDoctors_Institutions_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "InstitutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_MedicalSpecialityId",
                table: "Doctors",
                column: "MedicalSpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_FirstName_LastName",
                table: "Doctors",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionDoctors_InstitutionId",
                table: "InstitutionDoctors",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_Name_Address",
                table: "Institutions",
                columns: new[] { "Name", "Address" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalSpecialities_Name",
                table: "MedicalSpecialities",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstitutionDoctors");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "MedicalSpecialities");
        }
    }
}
