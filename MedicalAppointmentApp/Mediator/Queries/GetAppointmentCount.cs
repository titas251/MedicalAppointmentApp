using MediatR;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Queries
{
    public class GetAppointmentCount
    {
        public class Query : IRequest<int>
        {
        }

        public class Handler : IRequestHandler<Query, int>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointmentCount = await _context.Appointments
                    .CountAsync();

                return appointmentCount;
            }
        }
    }
}
