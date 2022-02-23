using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class AddInstitutionToDoctor
    {
        public class Command : IRequest<CustomResponse>
        {
            public int DoctorId { get; set; }
            public int InstitutionId { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly ApplicationDbContext _context;

            public Handler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                var doctor = await _context.Doctors.FindAsync(request.DoctorId);
                var institution = await _context.Institutions.FindAsync(request.InstitutionId);
                doctor.Institutions.Add(institution);
                _context.Doctors.Update(doctor);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    response.AddErrors(new CustomError { Error = "Failed", Message = "Failed to add institution to doctor" });
                }

                return response;
            }
        }
    }
}
