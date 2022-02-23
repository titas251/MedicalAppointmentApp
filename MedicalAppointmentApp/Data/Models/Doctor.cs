using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Institutions = new HashSet<Institution>();
        }

        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public int MedicalSpecialityId { get; set; }
        public virtual MedicalSpeciality MedicalSpeciality { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
