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
    public static class GetInstitutions
    {
        public class Query : IRequest<List<GetInstitutionModel>>
        {
        }

        public class Handler : IRequestHandler<Query, List<GetInstitutionModel>>
        {
            private readonly IInstitutionRepository _institutionRepository;
            private readonly IMapper _mapper;

            public Handler(IInstitutionRepository institutionRepository, IMapper mapper)
            {
                _institutionRepository = institutionRepository;
                _mapper = mapper;
            }

            public async Task<List<GetInstitutionModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var institutionsViewModel = new List<GetInstitutionModel>();
                var institutions = await _institutionRepository.GetAllWithInclude();

                foreach (var institution in institutions)
                {
                    var viewModel = _mapper.Map<GetInstitutionModel>(institution);
                    institutionsViewModel.Add(viewModel);
                }

                return institutionsViewModel;
            }
        }

        public class Response
        {
            public List<GetInstitutionModel> Institutions { get; set; }
        }
    }
}
