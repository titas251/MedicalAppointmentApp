using MediatR;
using MedicalAppointmentApp.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalAppointmentApp.Queries
{
    public static class GetRegisteredUsers
    {
        public class Query : IRequest<List<UserRolesViewModel>>
        {
        
        }
        public class Handler : IRequestHandler<Query, List<UserRolesViewModel>>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<List<UserRolesViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userRolesViewModel = new List<UserRolesViewModel>();

                foreach (var user in (await _userManager.Users.ToListAsync()))
                {
                    var viewModel = new UserRolesViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName,
                        Roles = await _userManager.GetRolesAsync(user)
                    };
                    userRolesViewModel.Add(viewModel);
                };
                return userRolesViewModel;
            }
        }

        public class Response
        {
            public Response(List<UserRolesViewModel> users)
            {
                this.users = users;
            }

            public List<UserRolesViewModel>  users { get;}
        }
    }
}
