using DAL.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task DeleteAsync(string userId);
        Task<ApplicationUser> GetByIdAsync(string userId);
        Task<int> GetUsersCountAsync();
        Task<IEnumerable<ApplicationUser>> GetAllWithPagingAsync(int pageNumber, int pageSize);
        Task<IList<string>> GetUserRolesAsync(string userId);
    }
}
