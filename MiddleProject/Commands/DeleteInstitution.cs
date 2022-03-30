using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using System;

namespace MiddleProject.Commands
{
    public class DeleteInstitution
    {
        public class Command : IRequest<CustomResponse>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {

            private IInstitutionRepository _institutionRepository;
            public Handler(IInstitutionRepository institutionRepository)
            {
                _institutionRepository = institutionRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    await _institutionRepository.DeleteAsync(request.Id);
                    await _institutionRepository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete institution" });
                }

                return response;
            }
        }
    }
}
