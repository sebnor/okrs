using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OKRs.Models;

namespace OKRs.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserById(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityResult> SaveUser(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }
    }
}
