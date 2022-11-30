using Microsoft.AspNetCore.Identity;
using OKRs.Models;

namespace OKRs
{
    public class CurrentContext : ICurrentContext
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentContext(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                throw new Exception("Http context is null!");
            return await _userManager.GetUserAsync(context.User);
        }
    }
}
