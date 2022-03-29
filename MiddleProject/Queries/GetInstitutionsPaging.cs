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
    public static class GetInstitutionsPaging
    {
        public class Query : IRequest<List<GetInstitutionModel>>
        {
            public Query(int page, int pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }
            public int Page { get; }
            public int PageSize { get; }
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
                var institutions = await _institutionRepository.GetAllWithPagingAsync(request.Page, request.PageSize);

                foreach (var institution in institutions)
                {
                    var viewModel = _mapper.Map<GetInstitutionModel>(institution);
                    institutionsViewModel.Add(viewModel);
                }

                return institutionsViewModel;
            }
        }
    }
}
