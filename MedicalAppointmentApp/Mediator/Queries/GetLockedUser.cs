using MediatR;
using DAL.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Queries
{
    public static class GetLockedUser
    {
        public class Query : IRequest<BlackedListedUser>
        {
            public Query(string userId)
            {
                UserId = userId;
            }

            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, BlackedListedUser>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<BlackedListedUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var blackedListedUser = new BlackedListedUser() { UserId = request.UserId };
                var user = await _userManager.FindByIdAsync(request.UserId);
                blackedListedUser.IsBlackListed = user.IsBlackListed;
                blackedListedUser.BlackListedEndDate = user.BlackListedEndDate;

                return blackedListedUser;
            }
        }
    }
}
