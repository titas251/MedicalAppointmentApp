using AutoMapper;
using MedicalAppointmentApp.Data.Models;

namespace MedicalAppointmentApp.Models.MapperProfiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<MedicalSpeciality, GetMedicalSpecialtyModel>().MaxDepth(1);
            CreateMap<Doctor, GetDoctorModel>().MaxDepth(1);
            CreateMap<Institution, GetInstitutionModel>().MaxDepth(1);
        }
    }
}
