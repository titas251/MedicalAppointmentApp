using MediatR;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace MiddleProject.Queries
{
    public static class GetRegisteredUserById
    {
        public class Query : IRequest<UpdateUserModel>
        {
            public Query(string id)
            {
                Id = id;
            }
            public string Id { get; }
        }

        public class Handler : IRequestHandler<Query, UpdateUserModel>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<UpdateUserModel> Handle(Query request, CancellationToken cancellationToken)
            {

                ApplicationUser user = await _userRepository.GetByIdAsync(request.Id);
                var updateUserModel = new UpdateUserModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber
                };
                return updateUserModel;
            }
        }
    }
}
