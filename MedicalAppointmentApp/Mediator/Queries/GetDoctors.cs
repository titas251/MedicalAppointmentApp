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
                
                foreach (var item in (await _context.Doctors.Include(doctor => doctor.MedicalSpeciality).Include(doctor => doctor.Institutions).ToListAsync()))
                {
                    var institutionsList = new List<GetInstitutionModel>();
                    
                    foreach (var institution in item.Institutions)
                    {
                        /* var test = (from i in _context.Institutions
                                    where i.InstitutionId.Equals(institution.InstitutionId)
                                    select new GetInstitutionModel { 
                                     InstitutionId = i.InstitutionId,
                                     Name = i.Name,
                                     Address = i.Address
                                    }).First();*/
                        var test = _context.Institutions.FirstOrDefault(i => i.InstitutionId == institution.InstitutionId);
                        institutionsList.Add(_mapper.Map<GetInstitutionModel>(test));
                    }
                    var viewModel = new GetDoctorModel()
                    {
                        DoctorId = item.DoctorId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        PhoneNumber = item.PhoneNumber,
                        MedicalSpecialityId = item.MedicalSpecialityId,
                        MedicalSpeciality = _mapper.Map<GetMedicalSpecialtyModel>(item.MedicalSpeciality),
                        Institutions = institutionsList
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
