using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class CreateMedicalSpeciality
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateMedicalSpecialityModel CreateMedicalSpeciality { get; set; }
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
                var medicalSpeciality = new MedicalSpeciality {
                    Name = request.CreateMedicalSpeciality.Name,
                    Description = request.CreateMedicalSpeciality.Description
                };

                await _context.MedicalSpecialities.AddAsync(medicalSpeciality);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success) {
                    response.AddErrors(new CustomError {Error = "Failed", Message = "Failed to create medical speciality"});
                    return response;
                }

                return response;
            }
        }
    }
}
