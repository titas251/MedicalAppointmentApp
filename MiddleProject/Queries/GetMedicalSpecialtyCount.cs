using MediatR;
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
            public Handler()
            {
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                /*var specialityCount = await _context.MedicalSpecialities
                    .CountAsync();*/

                var specialityCount = 100;

                //call another project

                return specialityCount;
            }
        }

    }
}
