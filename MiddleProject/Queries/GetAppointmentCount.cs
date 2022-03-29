using MediatR;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Queries
{
    public class GetAppointmentCount
    {
        public class Query : IRequest<int>
        {
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
                var appointmentCount = await _appointmentRepository.GetCountAsync();

                return appointmentCount;
            }
        }
    }
}
