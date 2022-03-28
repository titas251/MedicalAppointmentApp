using MediatR;
using DAL.Data.Models;
using MedicalAppointmentApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            public Query(int page, int pageSize)
            {
                Page = page;
                PageSize = pageSize;
            }
            public int Page { get; }
            public int PageSize { get; }
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

                var users = await _userManager.Users
                    .OrderBy(user => user.Email)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                foreach (var user in users)
                {
                    var viewModel = new UserRolesViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        UserName = user.UserName,
                        IsBlackListed = user.IsBlackListed,
                        BlackListedEndDate = user.BlackListedEndDate,
                        Roles = await _userManager.GetRolesAsync(user)
                    };
                    userRolesViewModel.Add(viewModel);
                };
                return userRolesViewModel;
            }
        }

        public class Response
        {
            public List<UserRolesViewModel> Users { get; set; }
        }
    }
}
