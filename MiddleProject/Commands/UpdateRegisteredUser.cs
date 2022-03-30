using DAL.Repositories.Interfaces;
using MediatR;
using MiddleProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Commands
{
    public class UpdateRegisteredUser
    {
        public class Command : IRequest<CustomResponse>
        {
            public UpdateUserModel UpdateUser { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    var user = await _userRepository.GetByIdAsync(request.UpdateUser.UserId);

                    //set updated user fields
                    user.FirstName = request.UpdateUser.FirstName;
                    user.LastName = request.UpdateUser.LastName;
                    user.PhoneNumber = request.UpdateUser.PhoneNumber;

                    await _userRepository.UpdateAsync(user);
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to update user" });
                }

                return response;
            }
        }
    }
}
