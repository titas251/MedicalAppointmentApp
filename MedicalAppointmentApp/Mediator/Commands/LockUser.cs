using MediatR;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class LockUser
    {
        public class Command : IRequest<CustomResponse>
        {
            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, CustomResponse>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<CustomResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var response = new CustomResponse();

                try
                {
                    var user = await _userManager.FindByIdAsync(request.UserId);

                    //add 14 day lock
                    user.IsBlackListed = true;
                    user.BlackListedEndDate = DateTime.Today.AddDays(14);

                    var result = await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to black list user" });
                }

                return response;
            }
        }
    }
}
