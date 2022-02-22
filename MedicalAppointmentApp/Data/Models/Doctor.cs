using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public int MedicalSpecialityId { get; set; }
        public virtual MedicalSpeciality MedicalSpeciality { get; set; }
        public virtual List<InstitutionDoctor> Institutions { get; set; }
    }
}
