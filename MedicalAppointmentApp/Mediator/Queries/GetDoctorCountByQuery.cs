using MediatR;
using MedicalAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Queries
{
    public static class GetDoctorCountByQuery
    {
        public class Query : IRequest<int>
        {
            public string StringQuery { get; }

            public Query(string stringQuery)
            {
                StringQuery = stringQuery;
            }
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
                var doctorCount = await _context.Doctors
                    .Where(doctor => doctor.FirstName.Contains(request.StringQuery)
                    || doctor.LastName.Contains(request.StringQuery)
                    || (doctor.FirstName + " " + doctor.LastName).Contains(request.StringQuery)
                    || doctor.MedicalSpeciality.Name.Contains(request.StringQuery)
                    || doctor.Schedules.Any(c => c.Institution.Name.Contains(request.StringQuery)))
                    .CountAsync();

                return doctorCount;
            }
        }
    }
}
