using System;
using System.Linq;
using CMSys.Core.Repositories;
using CMSys.WebApp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CMSys.Common.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace CMSys.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        IUnitOfWork _uow;
        public UsersController(IUnitOfWork uow) => _uow = uow;

        [Route("[area]/[action]")]
        public IActionResult Users()
        {
            var users = _uow.UserRepository.All();

            return View(users);
        }

        [Route("[area]/[controller]/{id:guid}")]
        public IActionResult Details(Guid id)
        {
            var user = _uow.UserRepository.Find(id);

            return View(user);
        }

        [HttpGet("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Update(Guid id)
        {
            var user = _uow.UserRepository.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserEditModel
            {
                Id = user.Id,
                Name = user.FullName,
                Roles = user.Roles,
                AllRoles = _uow.RoleRepository.All().Except(user.Roles)
            };

            return View(model);
        }

        [HttpPost("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult Update(UserEditModel model)
        {
            var user = _uow.UserRepository.Find(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            if (model.NewPassword != model.RepeatPassword)
            {
                ModelState.AddModelError("password", "Passwords must match");
            }
            if (string.IsNullOrEmpty(model.NewPassword))
            {
                ModelState.AddModelError("password", "Password cannot be empty");
            }
            else if (model.NewPassword.Length < 5 || model.NewPassword.Length > 128)
            {
                ModelState.AddModelError("password", "Password's length must be [5..128]");
            }
            if (!ModelState.IsValid)
            {
                model.Name = user.FullName;
                model.Roles = user.Roles;
                model.AllRoles = _uow.RoleRepository.All().Except(user.Roles);

                return View(model);
            }

            user.PasswordSalt = PasswordHelper.GenerateSalt(4);
            user.PasswordHash = PasswordHelper.ComputeHash(model.NewPassword, user.PasswordSalt);

            _uow.Commit();

            ViewBag.Name = user.FullName;
            return RedirectToAction("Update", new { id = model.Id });
        }

        [HttpPost("[area]/[controller]/[action]/{id:guid}")]
        public IActionResult AddRole(UserEditModel model)
        {
            var role = _uow.RoleRepository.Find(model.RoleId);
            var user = _uow.UserRepository.Find(model.Id);
            if (user == null || role == null)
            {
                return NotFound();
            }

            user.Roles.Add(role);
            _uow.Commit();

            return RedirectToAction("Update", new { id = model.Id });
        }
    }
}
