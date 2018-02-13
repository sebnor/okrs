using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OKRs.Models;
using OKRs.Models.UserViewModels;
using OKRs.Repositories;

namespace OKRs.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: User
        public ActionResult Index()
        {
            var users = _userRepository
                .GetAllUsers()
                .Select(x => new UserListItemViewModel { Id = Guid.Parse(x.Id), Email = x.Email, Name = x.Name })
                .OrderBy(x => x.Name)
                .ToList();

            var model = new UsersListViewModel { Users = users };
            return View(model);
        }

        // GET: User/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserFormModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name };
            var result = await _userRepository.CreateUser(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            AddErrors(result);
            return View();
        }

        // GET: User/Edit/{id}
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            var model = new SaveUserFormModel { Id = id, Name = user.Name, Email = user.Email, UserName = user.UserName };
            return View(model);
        }

        // POST: User/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [FromForm] SaveUserFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Id = id;
                return View(nameof(Edit), model);
            }

            var user = await _userRepository.GetUserById(id);
            user.Email = model.Email;
            user.Name = model.Name;
            user.UserName = model.UserName;
            var result = await _userRepository.SaveUser(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            model.Id = id;
            AddErrors(result);
            return View(nameof(Edit), model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}