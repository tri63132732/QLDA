using _4BroShop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PagedList;
using System.Threading.Tasks;


namespace _4BroShop.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController()
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        }
        // GET: Admin/Role
        public ActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
                return View(roles);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var result = _roleManager.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = _roleManager.FindById(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleToUpdate = _roleManager.FindById(model.Id);

                if (roleToUpdate != null)
                {
                    roleToUpdate.Name = model.Name;
                    _roleManager.Update(roleToUpdate);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy quyền cần cập nhật.");
                    return View(model);
                }
            }

            return View(model);
        }
        public async Task<ActionResult> Detail(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            ViewBag.RoleName = role.Name;
            return View(role);
        }


        // GET: Admin/Role/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var roleToDelete = _roleManager.FindById(id);

            if (roleToDelete == null)
            {
                return HttpNotFound();
            }

            return View(roleToDelete);
        }

        // POST: Admin/Role/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var roleToDelete = _roleManager.FindById(id);

            if (roleToDelete != null)
            {
                var result = _roleManager.Delete(roleToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể xóa role.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy role cần xóa.");
            }

            return RedirectToAction("Index");
        }

    }
}