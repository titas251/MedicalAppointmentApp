using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class CreateInstitutionDoctor
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateInstitutionDoctorModel InstitutionDoctorModel { get; set; }
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
                /*var institutionDoctor = new InstitutionDoctor
                {
                    DoctorId = request.InstitutionDoctorModel.DoctorId,
                    InstitutionId = request.InstitutionDoctorModel.InstitutionId
                };*/
                //await _context.InstitutionDoctors.AddAsync(institutionDoctor);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    response.AddErrors(new CustomError { Error = "Failed", Message = "Failed to create doctor" });
                }

                return response;
            }
        }
    }
}
