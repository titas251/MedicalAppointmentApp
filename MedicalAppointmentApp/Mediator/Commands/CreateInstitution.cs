using EntityFramework.Exceptions.Common;
using MediatR;
using MedicalAppointmentApp.Data;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
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

                try
                {
                    await _context.Institutions.AddAsync(institution);
                    await _context.SaveChangesAsync();
                }
                catch (UniqueConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Institution with given name and address already exists" });
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create institution" });
                }

                return response;
            }
        }
    }
}
