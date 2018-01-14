using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OKRs.Models;
using OKRs.Models.ObjectiveViewModels;
using OKRs.Repositories;

namespace OKRs.Controllers
{
    public class ObjectiveController : Controller
    {
        private readonly IObjectivesRepository _objectivesRepository;
        private readonly ICurrentContext _currentContext;

        public ObjectiveController(IObjectivesRepository objectivesRepository, ICurrentContext currentContext)
        {
            _objectivesRepository = objectivesRepository;
            _currentContext = currentContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var user = await _currentContext.GetCurrentUser();
            var model = await GetObjectiveModelForUser(Guid.Parse(user.Id));
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> ByUserId(Guid userId)
        {
            var model = await GetObjectiveModelForUser(userId);
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            var model = await GetObjectiveModelForAllUsers();
            return View(model);
        }

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

        public ActionResult Create()
        {
            return View();
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
        public async Task<ActionResult> Edit(Guid Id, [FromForm]UpdateObjectiveFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = Id });
            }
            var objective = await _objectivesRepository.GetObjectiveById(Id);
            objective.Title = formModel.Title;
            await _objectivesRepository.SaveObjective(objective);

            return RedirectToAction(nameof(Details), new { id = Id });
        }

        private async Task<ObjectivesListViewModel> GetObjectiveModelForUser(Guid userId)
        {
            var objectives = await _objectivesRepository.GetObjectivesByUserId(userId);
            return new ObjectivesListViewModel
            {
                Objectives = objectives.Select(x => new ObjectiveListItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    KeyResults = x.KeyResults.Select(y => new KeyResultListItemViewModel { Id = y.Id, Description = y.Description }).ToList()
                }).ToList()
            };
        }

        private async Task<List<ObjectivesListByUserViewModel>> GetObjectiveModelForAllUsers()
        {
            var objectiveGroup = (await _objectivesRepository.GetAllObjectives()).GroupBy(x => x.UserId);

            return objectiveGroup.Select(objectives =>
             new ObjectivesListByUserViewModel
             {
                 User = new ObjectiveUserViewModel { FirstName = "foo" }, //TODO
                 Objectives = objectives.Select(x => new ObjectiveListItemViewModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     KeyResults = x.KeyResults.Select(y => new KeyResultListItemViewModel { Id = y.Id, Description = y.Description }).ToList()
                 }).ToList()
             }).ToList();
        }
    }
}
