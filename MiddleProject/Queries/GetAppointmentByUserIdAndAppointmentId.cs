using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

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
            private readonly IAppointmentRepository _appointmentRepository;

            public Handler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointment = await _appointmentRepository.GetAppointmentsByUserIdAndAppointmentIdAsync(request.UserId, request.AppointmentId);
                return appointment;
            }
        }
    }
}
