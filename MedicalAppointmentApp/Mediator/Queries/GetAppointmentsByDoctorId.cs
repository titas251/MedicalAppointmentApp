using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MedicalAppointmentApp.Mediator.Queries
{
    public class GetAppointmentsByDoctorId
    {
        public class Query : IRequest<List<GetAppointmentModel>>
        {
            public Query(int id)
            {
                Id = id;
            }
            public int Id { get; }
        }

        public class Handler : IRequestHandler<Query, List<GetAppointmentModel>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetAppointmentModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointmentsViewModel = new List<GetAppointmentModel>();

                var appointments = await _context.Appointments.Include(a => a.Doctor)
                    .Where(a => a.DoctorId.Equals(request.Id))
                    .OrderBy(a => a.StartDateTime)
                    .ToListAsync();

                foreach (var appointment in appointments)
                {
                    var viewModel = _mapper.Map<GetAppointmentModel>(appointment);
                    appointmentsViewModel.Add(viewModel);
                }

                return appointmentsViewModel;
            }
        }
    }
}
