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
    public class CreateMedicalSpeciality
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateMedicalSpecialityModel MedicalSpecialityModel { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private IGenericRepository<MedicalSpeciality> _genericRepository;

            public Handler(IGenericRepository<MedicalSpeciality> genericRepository)
            {
                _genericRepository = genericRepository;
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
                    await _genericRepository.AddAsync(medicalSpeciality);
                }
                catch (UniqueConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Medical speciality with given name already exists" });
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create medical speciality" });
                }

                return response;
            }
        }
    }
}
