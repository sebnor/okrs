using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OKRs.Web.Models;

namespace OKRs.Web.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserById(Guid id);
        Task InactivateUser(Guid id);
        Task<IdentityResult> SaveUser(ApplicationUser user);
        List<ApplicationUser> GetAllUsers(bool includeInactive = false);
        Task<IdentityResult> CreateUser(ApplicationUser user);
        Task ActivateUser(Guid id);
    }
}