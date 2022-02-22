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
    public static class GetDoctors
    {
        public class Query : IRequest<List<GetDoctorModel>>
        {
        }

        public class Handler : IRequestHandler<Query, List<GetDoctorModel>>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GetDoctorModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctorsViewModel = new List<GetDoctorModel>();
                var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<MedicalSpeciality, GetMedicalSpecialtyModel>().MaxDepth(1);
                        cfg.CreateMap<Doctor, GetDoctorModel>().MaxDepth(1);
                    } 
                );
                var mapper = new Mapper(config);

                foreach (var item in (await _context.Doctors.Include(doctor => doctor.MedicalSpeciality).ToListAsync()))
                {
                    var viewModel = new GetDoctorModel()
                    {
                        DoctorId = item.DoctorId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        PhoneNumber = item.PhoneNumber,
                        MedicalSpecialityId = item.MedicalSpecialityId,
                        MedicalSpeciality = mapper.Map<GetMedicalSpecialtyModel>(item.MedicalSpeciality),
                        //Institutions = mapper.Map<List<GetInstitutionModel>>(item.Institutions)
                    };
                    doctorsViewModel.Add(viewModel);
                };
                return doctorsViewModel;
            }
        }

        public class Response
        {
            public List<GetDoctorModel> Doctors { get; set; }
        }
    }
}
