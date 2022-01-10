using System;
using Microsoft.AspNetCore.Mvc;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using CMSys.WebApp.Areas.Admin.ViewModels;

namespace CMSys.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseGroupsController : Controller
    {
        IUnitOfWork _uow;
        public CourseGroupsController(IUnitOfWork uow) => _uow = uow;

        [Route("[area]/[action]")]
        public IActionResult CourseGroups()
        {
            ViewBag.Title = "Course Groups";
            ViewBag.Rout = "CourseGroups";

            var model = new VisibleEntityEditModel
            {
                Entities = _uow.CourseGroupRepository.All()
            };

            return View("VisibleEntity", model);
        }
    }
}
