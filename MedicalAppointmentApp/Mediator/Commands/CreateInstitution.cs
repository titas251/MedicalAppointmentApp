using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class CreateInstitution
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateInstitutionModel InstitutionModel { get; set; }
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
                var institution = new Institution
                {
                    Name = request.InstitutionModel.Name,
                    Address = request.InstitutionModel.Address
                };
                await _context.Institutions.AddAsync(institution);

                //save changes and check if success
                var success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    response.AddErrors(new CustomError { Error = "Failed", Message = "Failed to create institution" });
                }

                return response;
            }
        }
    }
}
