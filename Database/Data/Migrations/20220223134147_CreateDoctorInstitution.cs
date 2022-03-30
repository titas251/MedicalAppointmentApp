using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Data.Migrations
{
    public partial class CreateDoctorInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    InstitutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => x.InstitutionId);
                });

            migrationBuilder.CreateTable(
                name: "MedicalSpecialities",
                columns: table => new
                {
                    MedicalSpecialityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSpecialities", x => x.MedicalSpecialityId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalSpecialityId = table.Column<int>(type: "int", nullable: false)
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
                name: "DoctorInstitution",
                columns: table => new
                {
                    DoctorsDoctorId = table.Column<int>(type: "int", nullable: false),
                    InstitutionsInstitutionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorInstitution", x => new { x.DoctorsDoctorId, x.InstitutionsInstitutionId });
                    table.ForeignKey(
                        name: "FK_DoctorInstitution_Doctors_DoctorsDoctorId",
                        column: x => x.DoctorsDoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorInstitution_Institutions_InstitutionsInstitutionId",
                        column: x => x.InstitutionsInstitutionId,
                        principalTable: "Institutions",
                        principalColumn: "InstitutionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorInstitution_InstitutionsInstitutionId",
                table: "DoctorInstitution",
                column: "InstitutionsInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_FirstName_LastName",
                table: "Doctors",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_MedicalSpecialityId",
                table: "Doctors",
                column: "MedicalSpecialityId");

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
                name: "DoctorInstitution");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "MedicalSpecialities");
        }
    }
}
