using MediatR;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class UpdateRegisteredUser
    {
        public class Command : IRequest<IdentityResult>
        {
            public UpdateUserModel UpdateUser { get; set; }
        }

        public class Handler : IRequestHandler<Command, IdentityResult>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IdentityResult> Handle(Command request, CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(request.UpdateUser.UserId);
                if (user == null)
                {
                    var error = new IdentityError
                    {
                        Code = "",
                        Description = "User not found"
                    };
                    return IdentityResult.Failed(error);
                }

                //set updated user fields
                user.FirstName = request.UpdateUser.FirstName;
                user.LastName = request.UpdateUser.LastName;
                user.PhoneNumber = request.UpdateUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded) {
                    var error = new IdentityError
                    {
                        Code = "",
                        Description = "Cannot update user"
                    };
                    return IdentityResult.Failed(error);
                }

                return IdentityResult.Success;
            }
        }
    }
}
