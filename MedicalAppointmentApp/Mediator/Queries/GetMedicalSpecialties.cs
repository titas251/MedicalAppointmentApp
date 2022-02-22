using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Queries
{
    public static class GetMedicalSpecialties
    {
        public class Query : IRequest<List<GetMedicalSpecialtyModel>>
        {
        }

        public class Handler : IRequestHandler<Query, List<GetMedicalSpecialtyModel>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GetMedicalSpecialtyModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var medicalSpecialtiesViewModel = new List<GetMedicalSpecialtyModel>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MedicalSpeciality, GetMedicalSpecialtyModel>().MaxDepth(1);
                    cfg.CreateMap<Doctor, GetDoctorModel>().MaxDepth(1);
                    cfg.CreateMap<Institution, GetInstitutionModel>().MaxDepth(1);
                }
                );
                var mapper = new Mapper(config);

                foreach (var specialty in (await _context.MedicalSpecialities.ToListAsync()))
                {
                    var doctorsList = new List<GetDoctorModel>();
                    foreach (var doctor in specialty.Doctors ?? new List<Doctor>())
                    {
                        var test = _context.Doctors.FirstOrDefault(i => i.DoctorId == doctor.DoctorId);
                        doctorsList.Add(mapper.Map<GetDoctorModel>(test));
                    }

                    var viewModel = new GetMedicalSpecialtyModel()
                    {
                        MedicalSpecialityId = specialty.MedicalSpecialityId,
                        Name = specialty.Name,
                        Description = specialty.Description,
                        Doctors = doctorsList
                    };
                    medicalSpecialtiesViewModel.Add(viewModel);
                };
                return medicalSpecialtiesViewModel;
            }
        }

        public class Response
        {
            public List<GetMedicalSpecialtyModel> MedicalSpecialties { get; set; }
        }
    }
}
