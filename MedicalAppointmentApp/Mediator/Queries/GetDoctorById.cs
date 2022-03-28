using AutoMapper;
using MediatR;
using DAL.Data;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MedicalAppointmentApp.Mediator.Queries
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
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<GetDoctorModel> Handle(Query request, CancellationToken cancellationToken)
            {

                var doctor = await _context.Doctors.Include(doctor => doctor.MedicalSpeciality).Include(doctor => doctor.Schedules)
                    .ThenInclude(schedules => schedules.Institution)
                    .Where(doctor => doctor.DoctorId.Equals(request.Id))
                    .OrderBy(doctor => doctor.LastName)
                        .ThenBy(doctor => doctor.FirstName)
                    .FirstOrDefaultAsync();

                var doctorViewModel = _mapper.Map<GetDoctorModel>(doctor);

                return doctorViewModel;
            }
        }
    }
}
