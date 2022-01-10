using System;
using Microsoft.AspNetCore.Mvc;
using CMSys.Core.Repositories;
using CMSys.WebApp.Areas.Admin.ViewModels;
using CMSys.Core.Entities.Catalog;

namespace CMSys.WebApp.Areas.Api
{
    [Area("Api")]
    [ApiController]
    [Route("api/[controller]")]
    public class TrainerGroupsController : ControllerBase
    {
        IUnitOfWork _uow;
        public TrainerGroupsController(IUnitOfWork uow) => _uow = uow;

        [HttpPost("[action]")]
        public ActionResult<VisibleEntityEditModel> Create(VisibleEntityEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = Guid.NewGuid();
            var group = new TrainerGroup
            {
                Id = model.Id,
                Name = model.Name,
                VisualOrder = model.VisualOrder,
                Description = model.Description,
            };

            _uow.TrainerGroupRepository.Add(group);
            _uow.Commit();

            return Ok(model);
        }

        [HttpPut("[action]")]
        public ActionResult<VisibleEntityEditModel> Update(VisibleEntityEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var group = _uow.TrainerGroupRepository.Find(model.Id);
            if (group == null)
            {
                return NotFound();
            }

            group.Name = model.Name;
            group.VisualOrder = model.VisualOrder;
            group.Description = model.Description;

            _uow.Commit();
            return Ok(model);
        }

        [HttpDelete("[action]/{id:guid}")]
        public ActionResult<VisibleEntityEditModel> Delete(Guid id)
        {
            var courses = _uow.TrainerRepository.Find(x => x.TrainerGroupId == id);

            if (courses != null)
            {
                return BadRequest();
            }

            var group = _uow.TrainerGroupRepository.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            _uow.TrainerGroupRepository.Remove(group);
            _uow.Commit();

            return Ok();
        }
    }
}
