using MediatR;
using MedicalAppointmentApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Commands
{
    public static class DeleteRegisteredUser
    {
        public class Command : IRequest<IdentityResult>
        {
            public string Id { get; set; }
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
                ApplicationUser user = await _userManager.FindByIdAsync(request.Id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    return result;
                }
                IdentityError error = new IdentityError
                {
                    Code = "",
                    Description = "User Not Found"
                };
                return IdentityResult.Failed(error);
            }
        }
    }
}
