using System;
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

        public ObjectiveController(IObjectivesRepository objectivesRepository)
        {
            _objectivesRepository = objectivesRepository;
        }

        // GET: Objective
        public async Task<ActionResult> Index()
        {
            var objectives = await _objectivesRepository.GetAllObjectives();
            var model = new ObjectivesListViewModel
            {
                Objectives = objectives.Select(x => new ObjectiveListItemViewModel { Id = x.Id, Title = x.Title }).ToList()
            };

            return View(model);
        }

        // GET: Objective/Details/{guid}
        public async Task<ActionResult> Details(Guid id)
        {
            var objective = await _objectivesRepository.GetObjectiveById(id);
            var model = new ObjectiveViewModel
            {
                Title = objective.Title,
                Id = objective.Id,
                Created = objective.Created,
                KeyResults = objective.KeyResults //TODO: convert to view model items
            };
            return View(model);
        }

        // GET: Objective/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Objective/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateObjectiveFormModel model)
        {
            try
            {
                var objective = new Objective(model.Title);
                _objectivesRepository.CreateObjective(objective);

                return RedirectToAction(nameof(Details), new { id = objective.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Objective/Edit/{guid}
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

        // POST: Objective/Edit/{guid}
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

        //// GET: Objectives/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Objectives/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}