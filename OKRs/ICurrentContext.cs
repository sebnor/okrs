using OKRs.Models;

namespace OKRs
{
    public interface ICurrentContext
    {
        Task<ApplicationUser> GetCurrentUser();
    }
}