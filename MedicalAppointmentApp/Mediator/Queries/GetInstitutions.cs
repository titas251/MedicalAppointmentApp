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
    public static class GetInstitutions
    {
        public class Query : IRequest<List<GetInstitutionModel>>
        {
        }

        public class Handler : IRequestHandler<Query, List<GetInstitutionModel>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetInstitutionModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var institutionsViewModel = new List<GetInstitutionModel>();

                foreach (var institution in (await _context.Institutions.Include(institution => institution.Doctors).ToListAsync()) )
                {
                    var doctorsList = new List<GetDoctorModel>();

                    foreach (var doctor in institution.Doctors ?? new List<InstitutionDoctor>())
                    {
                        var test = _context.Doctors.FirstOrDefault(i => i.DoctorId == doctor.DoctorId);
                        doctorsList.Add(_mapper.Map<GetDoctorModel>(test));
                    }
                    var viewModel = new GetInstitutionModel()
                    {
                        InstitutionId = institution.InstitutionId,
                        Name = institution.Name,
                        Address = institution.Address,
                        Doctors = doctorsList
                    };
                    institutionsViewModel.Add(viewModel);
                };
                return institutionsViewModel;
            }
        }

        public class Response
        {
            public List<GetInstitutionModel> Institutions { get; set; }
        }
    }
}
