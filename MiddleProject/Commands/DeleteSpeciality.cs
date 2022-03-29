using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories;

namespace MiddleProject.Commands
{
    public class DeleteSpeciality
    {
        public class Command : IRequest<CustomResponse>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private IMedicalSpecialityRepository _medicalSpecialityRepository;

            public Handler(IMedicalSpecialityRepository medicalSpecialityRepository)
            {
                _medicalSpecialityRepository = medicalSpecialityRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    await _medicalSpecialityRepository.DeleteAsync(request.Id);
                    await _medicalSpecialityRepository.SaveChangesAsync();
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
