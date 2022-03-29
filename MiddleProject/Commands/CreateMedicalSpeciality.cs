using EntityFramework.Exceptions.Common;
using MediatR;
using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MiddleProject.Models;
using DAL.Repositories;
using System;

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
            private IMedicalSpecialityRepository _medicalSpecialityRepository;

            public Handler(IMedicalSpecialityRepository medicalSpecialityRepository)
            {
                _medicalSpecialityRepository = medicalSpecialityRepository;
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
                    await _medicalSpecialityRepository.AddAsync(medicalSpeciality);
                    await _medicalSpecialityRepository.SaveChangesAsync();
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
