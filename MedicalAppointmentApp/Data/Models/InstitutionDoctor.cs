using System.ComponentModel.DataAnnotations;

namespace MedicalAppointmentApp.Data.Models
{
    public class InstitutionDoctor
    {
        [Key]
        public int DoctorId { get; set; }
        public int InstitutionId { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
