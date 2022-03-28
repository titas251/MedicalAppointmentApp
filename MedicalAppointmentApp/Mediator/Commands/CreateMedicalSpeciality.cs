using EntityFramework.Exceptions.Common;
using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.EntityFrameworkCore;
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
                var medicalSpeciality = new MedicalSpeciality
                {
                    Name = request.MedicalSpecialityModel.Name,
                    Description = request.MedicalSpecialityModel.Description
                };

                try
                {
                    await _context.MedicalSpecialities.AddAsync(medicalSpeciality);
                    await _context.SaveChangesAsync();
                }
                catch (UniqueConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Medical speciality with given name already exists" });
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create medical speciality" });
                }

                return response;
            }
        }
    }
}
