using MediatR;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MiddleProject.Models;

namespace MedicalAppointmentApp.Commands
{
    public static class DeleteRegisteredUser
    {
        public class Command : IRequest<CustomResponse>
        {
            public string Id { get; set; }
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
                    var user = await _userManager.FindByIdAsync(request.Id);
                    await _userManager.DeleteAsync(user);
                }
                catch (DbUpdateException)
                {
                    response.AddError(new CustomError { Error = "Failed", Message = "Failed to delete user" });
                }

                return response;
            }
        }
    }
}
