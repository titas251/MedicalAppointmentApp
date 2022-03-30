using DAL.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
{
    public static class GetMedicalSpecialtyCount
    {
        public class Query : IRequest<int>
        {
        }

        public class Handler : IRequestHandler<Query, int>
        {
            private IMedicalSpecialityRepository _medicalSpecialityRepository;

            public Handler(IMedicalSpecialityRepository medicalSpecialityRepository)
            {
                _medicalSpecialityRepository = medicalSpecialityRepository;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _medicalSpecialityRepository.GetCountAsync();
            }
        }

    }
}
