using MediatR;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Queries
{
    public static class GetMedicalSpecialtyCount
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
                var specialityCount = await _context.MedicalSpecialities
                    .CountAsync();

                return specialityCount;
            }
        }

    }
}
