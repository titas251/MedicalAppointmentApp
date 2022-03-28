using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


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
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetDoctorModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var doctorsViewModel = new List<GetDoctorModel>();
                var doctors = await _context.Doctors
                    .Include(doctor => doctor.MedicalSpeciality)
                    .Include(doctor => doctor.Schedules)
                        .ThenInclude(schedule => schedule.Institution)
                    .OrderBy(doctor => doctor.LastName)
                        .ThenBy(doctor => doctor.FirstName)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                foreach (var doctor in doctors)
                {
                    var viewModel = _mapper.Map<GetDoctorModel>(doctor);
                    doctorsViewModel.Add(viewModel);
                }

                return doctorsViewModel;
            }
        }

        public class Response
        {
            public List<GetDoctorModel> Doctors { get; set; }
        }
    }
}
