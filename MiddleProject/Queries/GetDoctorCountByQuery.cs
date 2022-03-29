using MediatR;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Queries
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
            private readonly IDoctorRepository _doctorRepository;

            public Handler(IDoctorRepository doctorRepository)
            {
                _doctorRepository = doctorRepository;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctorCount = await _doctorRepository.GetCountByQueryAsync(request.StringQuery);

                return doctorCount;
            }
        }
    }
}
