using AutoMapper;
using DAL.Repositories.Interfaces;
using MediatR;
using MiddleProject.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
{
    public class GetAppointments
    {
        public class Query : IRequest<List<GetAppointmentModel>>
        {
            public Query(int page, int pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }
            public int Page { get; }
            public int PageSize { get; }
        }

        public class Handler : IRequestHandler<Query, List<GetAppointmentModel>>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;

            public Handler(IAppointmentRepository appointmentRepository, IMapper mapper)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
            }

            public async Task<List<GetAppointmentModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var appointmentsViewModel = new List<GetAppointmentModel>();

                var appointments = await _appointmentRepository.GetAllWithPagingAsync(request.Page, request.PageSize);

                foreach (var appointment in appointments)
                {
                    var viewModel = _mapper.Map<GetAppointmentModel>(appointment);
                    appointmentsViewModel.Add(viewModel);
                }

                return appointmentsViewModel;
            }
        }
    }
}
