using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CMSys.Core.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CMSys.WebApp.Controllers
{
    [Authorize]
    public class TrainersController : Controller
    {
        private readonly IUnitOfWork _uow;

        public TrainersController(IUnitOfWork uow) => _uow = uow;

        [Route("[action]")]
        public IActionResult Trainers()
        {
            var groupTrainers = _uow.TrainerRepository.All().GroupBy(x => x.TrainerGroup);

            return View(groupTrainers);
        }
    }
}
