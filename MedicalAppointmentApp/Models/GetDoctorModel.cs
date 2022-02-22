using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Models
{
    public class GetDoctorModel
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int MedicalSpecialityId { get; set; }
        public GetMedicalSpecialtyModel MedicalSpeciality { get; set; }
        public List<GetInstitutionModel> Institutions { get; set; }
    }
}
