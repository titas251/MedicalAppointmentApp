using System.Collections.Generic;

namespace MiddleProject.Models
{
    public class CreateDoctorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int MedicalSpecialityId { get; set; }
        public List<GetMedicalSpecialtyModel> MedicalSpecialities { get; set; }
    }
}
