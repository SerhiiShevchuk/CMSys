using System;
using Microsoft.AspNetCore.Mvc;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using CMSys.WebApp.Areas.Admin.ViewModels;

namespace CMSys.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TrainerGroupsController : Controller
    {
        IUnitOfWork _uow;
        public TrainerGroupsController(IUnitOfWork uow) => _uow = uow;

        [Route("[area]/[action]")]
        public IActionResult TrainerGroups()
        {
            ViewBag.Title = "Trainer Groups";
            ViewBag.Rout = "TrainerGroups";

            var model = new VisibleEntityEditModel
            {
                Entities = _uow.TrainerGroupRepository.All()
            };

            return View("VisibleEntity", model);
        }
    }
}
