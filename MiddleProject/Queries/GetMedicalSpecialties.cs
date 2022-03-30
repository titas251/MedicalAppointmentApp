using AutoMapper;
using DAL.Repositories;
using MediatR;
using MiddleProject.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
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
            private readonly IMapper _mapper;
            private IMedicalSpecialityRepository _medicalSpecialityRepository;

            public Handler(IMedicalSpecialityRepository medicalSpecialityRepository, IMapper mapper)
            {
                _medicalSpecialityRepository = medicalSpecialityRepository;
                _mapper = mapper;
            }

            public async Task<List<GetMedicalSpecialtyModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var medicalSpecialtiesViewModel = new List<GetMedicalSpecialtyModel>();
                var specialities = await _medicalSpecialityRepository.GetAllWithPagingAsync(request.Page, request.PageSize);

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
