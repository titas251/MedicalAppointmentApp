using AutoMapper;
using MediatR;
using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
            public Query(int doctorId, string address, DateTime currentDate)
            {
                DoctorId = doctorId;
                Address = address;
                CurrentDate = currentDate;
            }
            public int DoctorId { get; }
            public string Address { get; }
            public DateTime CurrentDate { get; }
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
                    .Where(s => s.Schedule.DoctorId.Equals(request.DoctorId)
                    && s.Schedule.Institution.Address.Equals(request.Address)
                    && s.Schedule.StartDate <= request.CurrentDate.AddDays(7)
                    && s.Schedule.EndDate > request.CurrentDate)
                    .ToListAsync();

                return schedule;
            }
        }
    }
}
