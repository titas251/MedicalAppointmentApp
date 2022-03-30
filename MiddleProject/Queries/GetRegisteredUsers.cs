using DAL.Repositories.Interfaces;
using MediatR;
using MiddleProject.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
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
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<List<UserRolesViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userRolesViewModel = new List<UserRolesViewModel>();

                var users = await _userRepository.GetAllWithPagingAsync(request.Page, request.PageSize);

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
                        Roles = await _userRepository.GetUserRolesAsync(user.Id)
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
