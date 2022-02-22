namespace MedicalAppointmentApp.Models
{
    public class CreateDoctorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int MedicalSpecialityId { get; set; }
    }
}
