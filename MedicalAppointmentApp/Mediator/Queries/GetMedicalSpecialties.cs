using AutoMapper;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Queries
{
    public static class GetMedicalSpecialties
    {
        public class Query : IRequest<List<GetMedicalSpecialtyModel>>
        {
            public Query(int page, int pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }
            public int Page { get; }
            public int PageSize { get; }
        }

        public class Handler : IRequestHandler<Query, List<GetMedicalSpecialtyModel>>
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<GetMedicalSpecialtyModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var medicalSpecialtiesViewModel = new List<GetMedicalSpecialtyModel>();
                var specialities = await _context.MedicalSpecialities
                    .OrderBy(specialty => specialty.Name)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                foreach (var speciality in specialities)
                {
                    var viewModel = _mapper.Map<GetMedicalSpecialtyModel>(speciality);
                    medicalSpecialtiesViewModel.Add(viewModel);
                }

                return medicalSpecialtiesViewModel;
            }
        }

        public class Response
        {
            public List<GetMedicalSpecialtyModel> MedicalSpecialties { get; set; }
        }
    }
}
