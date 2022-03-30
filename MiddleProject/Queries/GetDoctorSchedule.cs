using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
{
    public class GetDoctorSchedule
    {
        public class Query : IRequest<List<ScheduleDetail>>
        {
            public Query(int doctorId, string address, DateTime currentDate)
            {
                DoctorId = doctorId;
                Address = address;
                CurrentDate = currentDate;
            }
            public int DoctorId { get; }
            public string Address { get; }
            public DateTime CurrentDate { get; }
        }

        public class Handler : IRequestHandler<Query, List<ScheduleDetail>>
        {
            private readonly IScheduleDetailRepository _scheduleDetailRepository;
            public Handler(IScheduleDetailRepository scheduleDetailRepository)
            {
                _scheduleDetailRepository = scheduleDetailRepository;
            }

            public async Task<List<ScheduleDetail>> Handle(Query request, CancellationToken cancellationToken)
            {
                var schedule = await _scheduleDetailRepository.GetDoctorScheduleAsync(request.DoctorId, request.Address, request.CurrentDate);

                return schedule;
            }
        }
    }
}
