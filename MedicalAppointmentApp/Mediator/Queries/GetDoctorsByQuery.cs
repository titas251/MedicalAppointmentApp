using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentApp.Mediator.Queries
{
    public class GetDoctorsByQuery
    {
        public class Query : IRequest<List<GetDoctorModel>>
        {
            public Query(string stringQuery)
            {
                StringQuery = stringQuery;
            }
            public string StringQuery { get; }
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

                var doctors = await _context.Doctors.Include(doctor => doctor.MedicalSpeciality).Include(doctor => doctor.Institutions)
                    .Where(doctor => doctor.FirstName.Contains(request.StringQuery) 
                    || doctor.LastName.Contains(request.StringQuery) 
                    || doctor.MedicalSpeciality.Name.Contains(request.StringQuery))
                    .ToListAsync();

                foreach (var doctor in doctors)
                {
                    var viewModel = _mapper.Map<GetDoctorModel>(doctor);
                    doctorsViewModel.Add(viewModel);
                }

                return doctorsViewModel;
            }
        }
    }
}
