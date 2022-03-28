using MediatR;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
{
    public class GetAppointmentCountByUserId
    {
        public class Query : IRequest<int>
        {
            public Query(string id)
            {
                Id = id;
            }
            public string Id { get; }
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
                    .Where(a => a.ApplicationUserId.Equals(request.Id))
                    .CountAsync();

                return appointmentCount;
            }
        }
    }
}
