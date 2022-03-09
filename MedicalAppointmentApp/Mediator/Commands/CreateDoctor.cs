using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class CreateDoctor
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateDoctorModel DoctorModel { get; set; }
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
                var doctor = new Doctor
                {
                    FirstName = request.DoctorModel.FirstName,
                    LastName = request.DoctorModel.LastName,
                    PhoneNumber = request.DoctorModel.PhoneNumber,
                    MedicalSpecialityId = request.DoctorModel.MedicalSpecialityId
                };
                await _context.Doctors.AddAsync(doctor);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create doctor" });
                }

                return response;
            }
        }
    }
}
