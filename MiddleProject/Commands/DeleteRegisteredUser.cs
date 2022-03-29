using MediatR;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories;
using System;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Commands
{
    public static class DeleteRegisteredUser
    {
        public class Command : IRequest<CustomResponse>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly DAL.Repositories.Interfaces.IUserRepository _userRepository;

            public Handler(DAL.Repositories.Interfaces.IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    await _userRepository.DeleteAsync(request.Id);
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete user" });
                }

                return response;
            }
        }
    }
}
