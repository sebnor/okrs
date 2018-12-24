using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OKRs.Models;
using OKRs.Models.ObjectiveViewModels;
using OKRs.Repositories;

namespace OKRs.Controllers
{
    public class ObjectiveController : Controller
    {
        private readonly IObjectivesRepository _objectivesRepository;
        private readonly ICurrentContext _currentContext;
        private readonly IUserRepository _userRepository;

        public ObjectiveController(
            IObjectivesRepository objectivesRepository,
            ICurrentContext currentContext,
            IUserRepository userRepository)
        {
            _objectivesRepository = objectivesRepository;
            _currentContext = currentContext;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await _currentContext.GetCurrentUser();
            var model = await GetObjectiveModelForUser(Guid.Parse(user.Id));
            return View(model);
        }

        [HttpGet]
        [Route("[controller]/[action]/{userId}")]
        public async Task<ActionResult> ByUserId(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
                return RedirectToAction(nameof(All));

            //var userModel = new ObjectiveUserViewModel { Id = userId, Name = user.Name };
            ViewData["Title"] = $"Objectives for {user.Name}";
            var model = await GetObjectiveModelForUser(userId);
            return View("Index", model);
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            var model = await GetObjectiveModelForAllUsers();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var objective = await _objectivesRepository.GetObjectiveById(id);
            var model = new ObjectiveViewModel
            {
                Id = objective.Id,
                Title = objective.Title,
                Created = objective.Created,
                KeyResults = objective.KeyResults.Select(y => new KeyResultListItemViewModel { Id = y.Id, Description = y.Description }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateForUser(Guid id)
        {
            ViewBag.Users = _userRepository.GetAllUsers().Select(x => new { Value = x.Id, Text = x.Name }).ToList();
            return View(new CreateObjectiveFormModel { UserId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateObjectiveFormModel model)
        {
            var user = await _currentContext.GetCurrentUser();
            var objective = new Objective(model.Title, user.UserId);
            await _objectivesRepository.CreateObjective(objective);
            return RedirectToAction(nameof(Details), new { id = objective.Id });
        }

        [HttpPost]
        //[Authorize(Roles="Admin")] TODO: implement roles for certain actions
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForUser(CreateObjectiveFormModel model)
        {
            var objective = new Objective(model.Title, model.UserId);
            await _objectivesRepository.CreateObjective(objective);
            return RedirectToAction(nameof(Details), new { id = objective.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var objective = await _objectivesRepository.GetObjectiveById(id);
            var model = new UpdateObjectiveViewModel
            {
                Title = objective.Title,
                Id = objective.Id,
                Created = objective.Created
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [FromForm]UpdateObjectiveFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new UpdateObjectiveViewModel { Title = formModel.Title };
                return View(nameof(Edit), viewModel);
            }
            var objective = await _objectivesRepository.GetObjectiveById(id);
            objective.Title = formModel.Title;
            objective.Touch();
            await _objectivesRepository.SaveObjective(objective);

            return RedirectToAction(nameof(Details), new { id });
        }

        private async Task<ObjectivesListViewModel> GetObjectiveModelForUser(Guid userId)
        {
            var objectives = await _objectivesRepository.GetObjectivesByUserId(userId);
            return new ObjectivesListViewModel
            {
                UserId = userId,
                IsObjectivesForCurrentUser = userId == Guid.Parse((await _currentContext.GetCurrentUser()).Id),
                Objectives = objectives.Select(x => new ObjectiveListItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    KeyResults = x.KeyResults.Select(y => new KeyResultListItemViewModel { Id = y.Id, Description = y.Description }).ToList()
                }).ToList()
            };
        }

        private async Task<AllObjectivesListViewModel> GetObjectiveModelForAllUsers()
        {
            ObjectiveUserViewModel GetModelFromUser(ApplicationUser user) => new ObjectiveUserViewModel(user?.UserId, user?.DisplayName, user?.Inactive);
            var users = _userRepository.GetAllUsers(includeInactive: true).ToDictionary(x => Guid.Parse(x.Id));

            var objectiveGroup = (await _objectivesRepository.GetAllObjectives()).GroupBy(x => x.UserId);
            return new AllObjectivesListViewModel
            {
                UserObjectivesList = objectiveGroup.Select(objectives =>
                 new ObjectivesListByUserViewModel
                 {
                     User = GetModelFromUser(users[objectives.Key]),
                     Objectives = objectives.Select(x => new ObjectiveListItemViewModel
                     {
                         Id = x.Id,
                         Title = x.Title,
                         KeyResults = x.KeyResults.Select(y => new KeyResultListItemViewModel { Id = y.Id, Description = y.Description }).ToList()
                     }).ToList()
                 }).Where(x => x.User.Inactive == false)
                 .OrderBy(x => x.User.Name)
                 .ToList()
            };
        }
    }
}
