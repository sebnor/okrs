using System.Threading.Tasks;
using OKRs.Web.Models;

namespace OKRs.Web
{
    public interface ICurrentContext
    {
        Task<ApplicationUser> GetCurrentUser();
    }
}