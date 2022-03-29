using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Commands
{
    public class DeleteDoctor
    {
        public class Command : IRequest<CustomResponse>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly IDoctorRepository _doctorRepository;

            public Handler(IDoctorRepository doctorRepository)
            {
                _doctorRepository = doctorRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    await _doctorRepository.DeleteAsync(request.Id);
                    await _doctorRepository.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete doctor" });
                }

                return response;
            }
        }
    }
}
