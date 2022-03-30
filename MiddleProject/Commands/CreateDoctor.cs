using EntityFramework.Exceptions.Common;
using MediatR;
using DAL.Data;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Commands
{
    public class CreateDoctor
    {
        public class Command : IRequest<CustomResponse>
        {
            public CreateDoctorModel DoctorModel { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly IGenericRepository<Doctor> _genericRepository;

            public Handler(IGenericRepository<Doctor> genericRepository)
            {
                _genericRepository = genericRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();
                var doctor = new Doctor
                {
                    FirstName = request.DoctorModel.FirstName,
                    LastName = request.DoctorModel.LastName,
                    PhoneNumber = request.DoctorModel.PhoneNumber,
                    MedicalSpecialityId = request.DoctorModel.MedicalSpecialityId,
                    NextFreeAppointmentDate = null
                };

                try
                {
                    await _genericRepository.AddAsync(doctor);
                }
                catch (UniqueConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Doctor with given first name and last name already exists" });
                }
                catch (ReferenceConstraintException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Medical speciality id doesn't exist" });
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to create doctor" });
                }

                return response;
            }
        }
    }
}
