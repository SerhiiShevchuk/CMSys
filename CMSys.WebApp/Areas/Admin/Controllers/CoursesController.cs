using System;
using Microsoft.AspNetCore.Mvc;
using CMSys.Core.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using CMSys.WebApp.Areas.Admin.ViewModels;
using CMSys.Core.Entities.Catalog;

namespace CMSys.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CoursesController : Controller
    {
        IUnitOfWork _uow;
        public CoursesController(IUnitOfWork uow) => _uow = uow;

        [Route("[area]/[action]")]
        public IActionResult Courses()
        {
            var courses = _uow.CourseRepository.All();
            
            return View(courses);
        }

        [HttpGet("[area]/[controller]/[action]")]
        public IActionResult Create()
        {
            var model = new CourseEditModel()
            {
                CourseTypes = _uow.CourseTypeRepository.All(),
                CourseGroups = _uow.CourseGroupRepository.All()
            };

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]")]
        public IActionResult Create(CourseEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CourseTypes = _uow.CourseTypeRepository.All();
                model.CourseGroups = _uow.CourseGroupRepository.All();

                return View(model);
            }

            var course = new Course()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                IsNew = model.IsNew,
                CourseType = _uow.CourseTypeRepository.Find(x => x.Name == model.CourseTypeName),
                CourseGroup = _uow.CourseGroupRepository.Find(x => x.Name == model.CourseGroupName),
                VisualOrder = model.VisualOrder,
                Description = model.Description
            };

            _uow.CourseRepository.Add(course);
            _uow.Commit();
            return RedirectToAction("Courses");
        }

        [HttpGet("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Update(Guid id)
        {
            var course = _uow.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            var model = new CourseEditModel
            {
                Id = course.Id,
                Name = course.Name,
                IsNew = course.IsNew,
                VisualOrder = course.VisualOrder,
                Description = course.Description,
                CourseTypeName = course.CourseType.Name,
                CourseGroupName = course.CourseGroup.Name,
                CourseTypes = _uow.CourseTypeRepository.All(),
                CourseGroups = _uow.CourseGroupRepository.All()
            };

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Update(CourseEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CourseTypes = _uow.CourseTypeRepository.All();
                model.CourseGroups = _uow.CourseGroupRepository.All();

                return View(model);
            }

            var course = _uow.CourseRepository.Find(model.Id);
            if (course == null)
            {
                return NotFound();
            }

            course.Name = model.Name;
            course.IsNew = model.IsNew;
            course.VisualOrder = model.VisualOrder;
            course.CourseType = _uow.CourseTypeRepository.Find(x => x.Name == model.CourseTypeName);
            course.CourseGroup = _uow.CourseGroupRepository.Find(x => x.Name == model.CourseGroupName);
            course.Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description;

            _uow.Commit();
            return RedirectToAction("Courses");
        }
        [HttpGet("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var course = _uow.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            _uow.CourseRepository.Remove(course);

            _uow.Commit();
            return RedirectToAction("Courses");
        }

        [HttpGet("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Trainers(Guid id)
        {
            var course = _uow.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.CourseName = course.Name;

            var trainers = course.Trainers.Select(x => x.Trainer);
            var model = new TrainersEditModel
            {
                Id = course.Id,
                CourseName = course.Name,
                CourseTrainers = course.Trainers,
                Trainers = _uow.TrainerRepository.All().Except(trainers).Select(x => x.User)
            };

            return View(model);
        }
        [HttpPost("[area]/[controller]/trainers/{id:guid}")]
        public IActionResult AddTrainers(TrainersEditModel model)
        {
            _uow.CourseTrainerRepository.Add(new CourseTrainer() { CourseId = model.Id, TrainerId = model.TrainerId });
            _uow.Commit();

            model.CourseTrainers = _uow.CourseTrainerRepository.Filter(ct => ct.CourseId == model.Id);
            var trainers = _uow.CourseRepository.Find(model.Id).Trainers.Select(x => x.Trainer);
            model.Trainers = _uow.TrainerRepository.All().Except(trainers).Select(x => x.User);

            return RedirectToAction("Trainers", new { id = model.Id });
        }
        [HttpPost("[area]/[controller]/EditVisualOrder/{id:guid}")]
        public IActionResult EditVisualOrder(TrainersEditModel model)
        {
            var courseTrainer = _uow.CourseTrainerRepository.Find(x => x.CourseId == model.Id);
            if (courseTrainer == null)
            {
                return NotFound();
            }

            courseTrainer.VisualOrder = model.VisualOrder;

            _uow.Commit();
            return RedirectToAction("Trainers", new { id = model.Id });
        }

        [Route("[area]/[controller]/DeleteTrainers/{courseId:guid}")]
        public IActionResult DeleteTrainers(Guid courseId, Guid trainerId)
        {
            var courseTrainer = _uow.CourseTrainerRepository.Find(courseId, trainerId);
            if (courseTrainer == null)
            {
                return NotFound();
            }

            _uow.CourseTrainerRepository.Remove(courseTrainer);

            _uow.Commit();
            return RedirectToAction("Trainers", new { id = courseId });
        }
    }
}
