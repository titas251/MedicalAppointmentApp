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
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetDoctorModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctorsViewModel = new List<GetDoctorModel>();
                
                foreach (var doctor in (await _context.Doctors.Include(doctor => doctor.MedicalSpeciality).Include(doctor => doctor.Institutions).ToListAsync()))
                {
                    var institutionsList = new List<GetInstitutionModel>();

                    foreach (var institution in doctor.Institutions)
                    {
                        var institutionModel = _context.Institutions.FirstOrDefault(i => i.InstitutionId == institution.InstitutionId);
                        institutionsList.Add(_mapper.Map<GetInstitutionModel>(institutionModel));
                    }

                    var viewModel = _mapper.Map<GetDoctorModel>(doctor);
                    viewModel.Institutions = institutionsList;
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
