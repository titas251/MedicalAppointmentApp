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
            public Query(int doctorId)
            {
                DoctorId = doctorId;
            }
            public int DoctorId { get; }
        }

        public class Handler : IRequestHandler<Query, List<ScheduleDetail>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<ScheduleDetail>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schedule = await _context.ScheduleDetails.Include(s => s.Schedule)                  
                    .Where(s => s.Schedule.DoctorId.Equals(request.DoctorId))
                    .ToListAsync();

                return schedule;
            }
        }
    }
}
