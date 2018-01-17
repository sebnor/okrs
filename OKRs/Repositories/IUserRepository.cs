using System;
using System.Threading.Tasks;
using OKRs.Models;

namespace OKRs.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(Guid id);
    }
}