using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using EntityFramework.Exceptions.Common;
using MediatR;
using MiddleProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly IGenericRepository<Institution> _genericRepository;
            public Handler(IGenericRepository<Institution> genericRepository)
            {
                _genericRepository = genericRepository;
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
                    await _genericRepository.AddAsync(institution);
                }
                catch (UniqueConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Institution with given name and address already exists" });
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create institution" });
                }

                return response;
            }
        }
    }
}
