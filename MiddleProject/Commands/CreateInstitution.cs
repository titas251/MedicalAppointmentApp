using EntityFramework.Exceptions.Common;
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
    public class CreateInstitution
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateInstitutionModel InstitutionModel { get; set; }
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
                var institution = new Institution
                {
                    Name = request.InstitutionModel.Name,
                    Address = request.InstitutionModel.Address
                };

                try
                {
                    await _institutionRepository.AddAsync(institution);
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
