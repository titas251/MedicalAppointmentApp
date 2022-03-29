using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Queries
{
    public static class GetDoctors
    {
        public class Query : IRequest<List<GetDoctorModel>>
        {
            public Query(int page, int pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }
            public int Page { get; }
            public int PageSize { get; }
        }

        public class Handler : IRequestHandler<Query, List<GetDoctorModel>>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMapper _mapper;

            public Handler(IDoctorRepository doctorRepository, IMapper mapper)
            {
                _doctorRepository = doctorRepository;
                _mapper = mapper;
            }

            public async Task<List<GetDoctorModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctorsViewModel = new List<GetDoctorModel>();
                var doctors = await _doctorRepository.GetAllWithPagingAsync(request.Page, request.PageSize);

                foreach (var doctor in doctors)
                {
                    var viewModel = _mapper.Map<GetDoctorModel>(doctor);
                    doctorsViewModel.Add(viewModel);
                }

                return doctorsViewModel;
            }
        }
    }
}
