﻿using System;
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
    }
}
