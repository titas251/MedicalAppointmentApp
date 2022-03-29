using MediatR;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

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
            private readonly IAppointmentRepository _appointmentRepository;

            public Handler(IAppointmentRepository appointmentRepository)
            {
                _appointmentRepository = appointmentRepository;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointmentCount = await _appointmentRepository.GetCountByUserIdAsync(request.Id);

                return appointmentCount;
            }
        }
    }
}
