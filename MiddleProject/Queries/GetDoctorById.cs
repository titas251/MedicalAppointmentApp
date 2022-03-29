using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Queries
{
    public class GetDoctorById
    {
        public class Query : IRequest<GetDoctorModel>
        {
            public Query(int id)
            {
                Id = id;
            }
            public int Id { get; }
        }

        public class Handler : IRequestHandler<Query, GetDoctorModel>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMapper _mapper;

            public Handler(IDoctorRepository doctorRepository, IMapper mapper)
            {
                _doctorRepository = doctorRepository;
                _mapper = mapper;
            }

            public async Task<GetDoctorModel> Handle(Query request, CancellationToken cancellationToken)
            {

                var doctor = _doctorRepository.GetByIdWithIncludeAsync(request.Id);

                var doctorViewModel = _mapper.Map<GetDoctorModel>(doctor);

                return doctorViewModel;
            }
        }
    }
}
