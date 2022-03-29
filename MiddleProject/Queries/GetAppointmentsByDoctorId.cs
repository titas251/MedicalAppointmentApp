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
    public class GetAppointmentsByDoctorId
    {
        public class Query : IRequest<List<GetAppointmentModel>>
        {
            public Query(int id)
            {
                Id = id;
            }
            public int Id { get; }
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

                var appointments = await _appointmentRepository.GetAppointmentsByDoctorIdAsync(request.Id);

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
