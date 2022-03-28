using MediatR;
using DAL.Data.Models;
using MiddleProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<UpdateUserModel> Handle(Query request, CancellationToken cancellationToken)
            {

                ApplicationUser user = await _userManager.FindByIdAsync(request.Id);
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

        public class Response
        {
            public UpdateUserModel User { get; set; }
        }
    }
}
