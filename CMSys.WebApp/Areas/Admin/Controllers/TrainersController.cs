using System;
using System.Linq;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using CMSys.WebApp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSys.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrainersController : Controller
    {
        IUnitOfWork _uow;
        public TrainersController(IUnitOfWork uow) => _uow = uow;

        [Route("[area]/[action]")]
        public IActionResult Trainers()
        {
            var trainers = _uow.TrainerRepository.All();

            return View(trainers);
        }

        [HttpGet("[area]/[controller]/[action]")]
        public IActionResult Create()
        {
            var trainers = _uow.TrainerRepository.All().Select(x => x.User);
            var model = new TrainerEditModel
            {
                Users = _uow.UserRepository.All().Except(trainers),
                TrainerGroups = _uow.TrainerGroupRepository.All(),
            };

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]")]
        public IActionResult Create(TrainerEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = _uow.UserRepository.All();
                model.TrainerGroups = _uow.TrainerGroupRepository.All();

                return View(model);
            }

            var trainer = new Trainer
            {
                Id = model.Id,
                TrainerGroupId = model.TrainerGroupId,
                VisualOrder = model.VisualOrder,
                Description = model.Description,
            };

            _uow.TrainerRepository.Add(trainer);
            _uow.Commit();

            return RedirectToAction("Trainers");
        }
        [HttpGet("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Update(Guid id)
        {
            var trainer = _uow.TrainerRepository.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            var trainers = _uow.TrainerRepository.All().Select(x => x.User);
            var model = new TrainerEditModel
            {
                Name = trainer.User.FullName,
                VisualOrder = trainer.VisualOrder,
                Description = trainer.Description,
                TrainerGroupId = trainer.TrainerGroupId,
                TrainerGroups = _uow.TrainerGroupRepository.All(),
                Users = _uow.UserRepository.All().Except(trainers)
            };

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Update(TrainerEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = _uow.UserRepository.All();
                model.TrainerGroups = _uow.TrainerGroupRepository.All();

                return View(model);
            }

            var trainer = _uow.TrainerRepository.Find(model.Id);
            if (trainer == null)
            {
                return NotFound();
            }

            trainer.VisualOrder = model.VisualOrder;
            trainer.Description = model.Description;
            trainer.TrainerGroupId = model.TrainerGroupId;

            _uow.Commit();
            return RedirectToAction("Trainers");
        }

        [HttpGet("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var trainer = _uow.TrainerRepository.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _uow.TrainerRepository.Remove(trainer);

            _uow.Commit();
            return RedirectToAction("Trainers");
        }
    }
}
