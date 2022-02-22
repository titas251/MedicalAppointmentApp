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

                foreach (var specialty in (await _context.MedicalSpecialities.ToListAsync()))
                {
                    var viewModel = new GetMedicalSpecialtyModel()
                    {
                        MedicalSpecialityId = specialty.MedicalSpecialityId,
                        Name = specialty.Name,
                        Description = specialty.Description,
                        Doctors = JsonConvert.DeserializeObject<List<GetDoctorModel>>(JsonConvert.SerializeObject(specialty.Doctors))
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
