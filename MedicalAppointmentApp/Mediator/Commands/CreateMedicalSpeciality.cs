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
            public CreateMedicalSpecialityModel MedicalSpecialityModel { get; set; }
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
                    Name = request.MedicalSpecialityModel.Name,
                    Description = request.MedicalSpecialityModel.Description
                };
                await _context.MedicalSpecialities.AddAsync(medicalSpeciality);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success) {
                    response.AddError(new CustomError {Error = "Failed", Message = "Failed to create medical speciality"});
                }

                return response;
            }
        }
    }
}
