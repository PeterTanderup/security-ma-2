using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MySensaiDk.Infrastructure;
using MySensaiDk.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using PagedList;

namespace MySensaiDk.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminUserController : Controller
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: Admin/AdminUser
        public ActionResult Index( int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            IEnumerable<AppUser> users = UserManager.Users.ToList();

            return View(users.ToPagedList(pageNumber,pageSize));
        }

        // GET: Admin/AdminUser
        public async Task<ActionResult> Details(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            AdminUserDetails ud = new AdminUserDetails { CurrentUser = user };
            return View(ud);
        }

        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }
        
        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            AdminEditUserDetails userdetails = new AdminEditUserDetails();
            userdetails.Id = id;
            userdetails.Email = user.Email;
            userdetails.FirstName = user.FirstName;
            userdetails.LastName = user.LastName;
            userdetails.Birthday = user.Birthday;
            userdetails.Gender = user.Gender;
            userdetails.PhoneNumber = user.PhoneNumber;

            return View(userdetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdminEditUserDetails userdetails)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindByIdAsync(userdetails.Id);
                if (user == null)
                {
                    ModelState.AddModelError("", "Noget gik galt");
                }
                else
                {
                    user.Email = userdetails.Email;
                    user.UserName = userdetails.Email;
                    user.FirstName = userdetails.FirstName;
                    user.LastName = userdetails.LastName;
                    user.Gender = userdetails.Gender;
                    user.Birthday = userdetails.Birthday;
                    user.PhoneNumber = string.IsNullOrEmpty(userdetails.PhoneNumber) ? null : userdetails.PhoneNumber;
                    await UserManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
            }
            return View(userdetails);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    Gender = model.Gender,
                    PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? null : model.PhoneNumber
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user = await UserManager.FindByNameAsync(model.Email);
                    await UserManager.AddToRoleAsync(user.Id, "Users");

                    return RedirectToAction("Index");
                }
                AddErrorsFromResult(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public JsonResult ValidateEmail(string Email, string Id)
        {
            AppUser currentuser = UserManager.FindById(Id);
            var user = UserManager.FindByName(Email);

            if (user == null)
            {
                // new e-mail entered
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if (currentuser.UserName.CompareTo(Email) == 0)
            {
                // same e-mail as before
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ModelState.AddModelError("Email", "Den intastede e-mail er allerede i brug.");
                return Json("Den intastede e-mail er allerede i brug.", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ValidateEmailReg(string Email)
        {
            var user = UserManager.FindByName(Email);

            if (user == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ModelState.AddModelError("Email", "Den intastede e-mail er allerede i brug.");
                return Json("Den intastede e-mail er allerede i brug.", JsonRequestBehavior.AllowGet);
            }

        }
    }
}