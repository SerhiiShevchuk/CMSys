using System;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSys.WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CoursesController(IUnitOfWork uow) => _uow = uow;

        [Route("[action]")]
        public IActionResult Courses()
        {
            var courses = _uow.CourseRepository.All();

            return View(courses);
        }

        [Route("[action]/{id:guid}")]
        public IActionResult Details(Guid id, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var course = _uow.CourseRepository.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
