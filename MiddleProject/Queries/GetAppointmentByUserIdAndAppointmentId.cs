using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
{
    public class GetAppointmentsByUserIdAndAppointmentId
    {
        public class Query : IRequest<bool>
        {
            public Query(string userId, int appointmentId)
            {
                UserId = userId;
                AppointmentId = appointmentId;
            }

            public string UserId { get; }
            public int AppointmentId { get; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointmentCount = await _context.Appointments
                    .AnyAsync(a => a.ApplicationUserId.Equals(request.UserId) && a.AppointmentId.Equals(request.AppointmentId));
                return appointmentCount;
            }
        }
    }
}
