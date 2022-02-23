using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class Institution
    {
        public Institution()
        {
            this.Doctors = new HashSet<Doctor>();
        }

        [Key]
        public int InstitutionId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
