using MediatR;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MiddleProject.Models;

namespace MedicalAppointmentApp.Mediator.Commands
{
    public class UpdateRegisteredUser
    {
        public class Command : IRequest<CustomResponse>
        {
            public UpdateUserModel UpdateUser { get; set; }
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
                    var user = await _userManager.FindByIdAsync(request.UpdateUser.UserId);

                    //set updated user fields
                    user.FirstName = request.UpdateUser.FirstName;
                    user.LastName = request.UpdateUser.LastName;
                    user.PhoneNumber = request.UpdateUser.PhoneNumber;

                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to update user" });
                }

                return response;
            }
        }
    }
}
