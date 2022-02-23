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
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetMedicalSpecialtyModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var medicalSpecialtiesViewModel = new List<GetMedicalSpecialtyModel>();

                foreach (var specialty in (await _context.MedicalSpecialities.Include(specialty => specialty.Doctors).ToListAsync()))
                {
                    var doctorsList = new List<GetDoctorModel>();
                    foreach (var doctor in specialty.Doctors ?? new List<Doctor>())
                    {
                        var test = _context.Doctors.FirstOrDefault(i => i.DoctorId == doctor.DoctorId);
                        doctorsList.Add(_mapper.Map<GetDoctorModel>(test));
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
