using MediatR;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Mediator.Queries
{
    public static class GetLockedUser
    {
        public class Query : IRequest<LockedUser>
        {
            public Query(string userId) {
                UserId = userId; 
            }

            public string UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, LockedUser>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<LockedUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var lockedUser = new LockedUser() { UserId = request.UserId };
                var user = await _userManager.FindByIdAsync(request.UserId);
                lockedUser.IsLocked = user.LockoutEnabled;
                lockedUser.LockoutEndDate = user.LockoutEnd.GetValueOrDefault().UtcDateTime;

                return lockedUser;
            }
        }
    }
}
