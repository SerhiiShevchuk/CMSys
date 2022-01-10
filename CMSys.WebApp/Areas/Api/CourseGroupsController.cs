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
    public class CourseGroupsController : ControllerBase
    {
        IUnitOfWork _uow;
        public CourseGroupsController(IUnitOfWork uow) => _uow = uow;

        [HttpPost("[action]")]
        public ActionResult<VisibleEntityEditModel> Create(VisibleEntityEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Id = Guid.NewGuid();
            var group = new CourseGroup
            {
                Id = model.Id,
                Name = model.Name,
                VisualOrder = model.VisualOrder,
                Description = model.Description,
            };

            _uow.CourseGroupRepository.Add(group);
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

            var group = _uow.CourseGroupRepository.Find(model.Id);
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
            var courses =  _uow.CourseRepository.Find(x => x.CourseGroupId == id);

            if (courses != null)
            {
                return BadRequest();
            }

            var group = _uow.CourseGroupRepository.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            _uow.CourseGroupRepository.Remove(group);
            _uow.Commit();

            return Ok();
        }
    }
}
