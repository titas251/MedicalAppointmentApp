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

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<GetInstitutionModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var institutionsViewModel = new List<GetInstitutionModel>();

                foreach (var institution in (await _context.Institutions.ToListAsync()))
                {
                    var viewModel = new GetInstitutionModel()
                    {
                        InstitutionId = institution.InstitutionId,
                        Name = institution.Name,
                        Address = institution.Address,
                        Doctors = JsonConvert.DeserializeObject<List<GetDoctorModel>>(JsonConvert.SerializeObject(institution.Doctors))
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
