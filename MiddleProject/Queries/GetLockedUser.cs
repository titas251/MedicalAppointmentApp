using DAL.Repositories.Interfaces;
using MediatR;
using MiddleProject.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MiddleProject.Queries
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
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<BlackedListedUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var blackedListedUser = new BlackedListedUser() { UserId = request.UserId };
                var user = await _userRepository.GetByIdAsync(request.UserId);
                blackedListedUser.IsBlackListed = user.IsBlackListed;
                blackedListedUser.BlackListedEndDate = user.BlackListedEndDate;

                return blackedListedUser;
            }
        }
    }
}
