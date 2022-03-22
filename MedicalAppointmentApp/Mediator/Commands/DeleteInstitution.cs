using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class DeleteInstitution
    {
        public class Command : IRequest<CustomResponse>
        {
            public int Id { get; set; }
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

                try
                {
                    _context.Institutions.Remove(new Institution { InstitutionId = request.Id });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete institution" });
                }

                return response;
            }
        }
    }
}
