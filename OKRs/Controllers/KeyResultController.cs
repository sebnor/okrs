using Microsoft.AspNetCore.Mvc;
using OKRs.Models;
using OKRs.Models.ObjectiveViewModels;
using OKRs.Repositories;

namespace OKRs.Controllers
{
    public class KeyResultController : Controller
    {
        private readonly IObjectivesRepository _objectivesRepository;

        public KeyResultController(IObjectivesRepository objectivesRepository)
        {
            _objectivesRepository = objectivesRepository;
        }

        [Route("[controller]/[action]/{objectiveId}/{keyResultId}")]
        public async Task<ActionResult> Details(Guid objectiveId, Guid keyResultId)
        {
            var objective = await _objectivesRepository.GetObjectiveById(objectiveId);
            var keyResult = objective.KeyResults.Single(x => x.Id == keyResultId);

            var model = new KeyResultDetailsViewModel
            {
                ObjectiveTitle = objective.Title,
                Description = keyResult.Description,
                Id = keyResult.Id,
                ObjectiveId = objective.Id,
                Created = objective.Created
            };
            return View(model);
        }

        [Route("[controller]/[action]/{objectiveId}")]
        public async Task<ActionResult> Add(Guid objectiveId)
        {
            var objective = await _objectivesRepository.GetObjectiveById(objectiveId);

            var model = new AddKeyResultViewModel
            {
                ObjectiveTitle = objective.Title,
                ObjectiveId = objective.Id,
            };
            return View(model);
        }

        [Route("[controller]/[action]/{objectiveId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Guid objectiveId, [FromForm] SaveKeyResultFormModel formModel)
        {
            var objective = await _objectivesRepository.GetObjectiveById(objectiveId);
            var keyResult = new KeyResult
            {
                Description = formModel.Description
            };
            objective.AddKeyResult(keyResult);

            await _objectivesRepository.SaveObjective(objective);
            return RedirectToAction("Details", "Objective", new { id = objectiveId });
        }

        [Route("[controller]/[action]/{objectiveId}/{keyResultId}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid objectiveId, Guid keyResultId)
        {
            var objective = await _objectivesRepository.GetObjectiveById(objectiveId);
            var keyResult = objective.KeyResults.Single(x => x.Id == keyResultId);

            var model = new EditKeyResultViewModel
            {
                Id = keyResult.Id,
                ObjectiveId = objective.Id,
                ObjectiveTitle = objective.Title,
                Description = keyResult.Description,
            };
            return View(model);
        }

        [Route("[controller]/[action]/{objectiveId}/{keyResultId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid objectiveId, Guid keyResultId, [FromForm] SaveKeyResultFormModel formModel)
        {
            var objective = await _objectivesRepository.GetObjectiveById(objectiveId);
            var keyResult = objective.KeyResults.Single(x => x.Id == keyResultId);
            keyResult.Description = formModel.Description;
            keyResult.Touch();

            await _objectivesRepository.SaveObjective(objective);
            return RedirectToAction(nameof(Details), new { objectiveId, keyResultId = keyResult.Id });
        }

        [Route("[controller]/[action]/{keyResultId}")]
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid keyResultId)
        {
            var objective = await _objectivesRepository.GetObjectiveByKeyResultId(keyResultId);
            var keyResult = objective.KeyResults.Single(x => x.Id == keyResultId);
            objective.KeyResults.Remove(keyResult);

            await _objectivesRepository.SaveObjective(objective);
            return Ok();
        }
    }
}