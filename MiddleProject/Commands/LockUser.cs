using DAL.Repositories.Interfaces;
using MediatR;
using MiddleProject.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Commands
{
    public class LockUser
    {
        public class Command : IRequest<CustomResponse>
        {
            public string UserId { get; set; }
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
                    var user = await _userRepository.GetByIdAsync(request.UserId);

                    //add 14 day lock
                    user.IsBlackListed = true;
                    user.BlackListedEndDate = DateTime.Today.AddDays(14);

                    await _userRepository.UpdateAsync(user);
                }
                catch (Exception)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to black list user" });
                }

                return response;
            }
        }
    }
}
