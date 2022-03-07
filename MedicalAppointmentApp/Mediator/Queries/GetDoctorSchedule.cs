using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Queries
{
    public class GetDoctorSchedule
    {
        public class Query : IRequest<List<ScheduleDetail>>
        {
            public Query(int doctorId, string address)
            {
                DoctorId = doctorId;
                Address = address;
            }
            public int DoctorId { get; }
            public string Address { get; }
        }

        public class Handler : IRequestHandler<Query, List<ScheduleDetail>>
        {
            private readonly ApplicationDbContext _context;
            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
            }

            public async Task<List<ScheduleDetail>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schedule = await _context.ScheduleDetails.Include(s => s.Schedule).ThenInclude(s => s.Institution)
                    .Where(s => s.Schedule.DoctorId.Equals(request.DoctorId) && s.Schedule.Institution.Address.Equals(request.Address))
                    .ToListAsync();

                return schedule;
            }
        }
    }
}
