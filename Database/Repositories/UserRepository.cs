using DAL.Data;
using DAL.Data.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        public async Task DeleteAsync(string userId)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(userId));
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllWithPagingAsync(int pageNumber, int pageSize)
        {
            return await _userManager.Users
                    .OrderBy(user => user.Email)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            return await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }
    }
}
