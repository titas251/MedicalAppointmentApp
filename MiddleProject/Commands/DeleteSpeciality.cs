using DAL.Repositories;
using MediatR;
using MiddleProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

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
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete medical specialty" });
                }

                return response;
            }
        }
    }
}
