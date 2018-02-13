using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OKRs.Models;

namespace OKRs.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(Guid id);
        Task<IdentityResult> SaveUser(ApplicationUser user);
        List<ApplicationUser> GetAllUsers();
        Task<IdentityResult> CreateUser(ApplicationUser user);
    }
}