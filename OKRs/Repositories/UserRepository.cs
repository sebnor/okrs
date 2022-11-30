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

        public List<ApplicationUser> GetAllUsers(bool includeInactive)
        {
            if (includeInactive)
                return _userManager.Users.ToList();
            return _userManager.Users.Where(x => !x.Inactive).ToList();
        }

        public async Task<IdentityResult> SaveUser(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task InactivateUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Inactive = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task ActivateUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.Inactive = false;
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser user)
        {
            return await _userManager.CreateAsync(user);
        }
    }
}
