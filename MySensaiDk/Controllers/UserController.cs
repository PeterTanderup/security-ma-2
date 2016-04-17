using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Threading.Tasks;
using MySensaiDk.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySensaiDk.Models;
using PagedList;


namespace MySensaiDk.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private AppUser CurrentUser
        {
            get { return UserManager.FindByName(HttpContext.User.Identity.Name); }
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }

        // GET: User
        public ActionResult Index(int? coursePage, int? addressPage, int? enrollmentPage)
        {
            int coursePageSize = 3;
            int addressPageSize = 3;
            int enrollmentPageSize = 3;
            int coursePageNumber = (coursePage ?? 1);
            int addressPageNumber = (addressPage ?? 1);
            int enrollmentPageNumber = (enrollmentPage ?? 1);

            UserDetails currentUserDetails = new UserDetails();
            currentUserDetails.CurrentUser = CurrentUser;
            currentUserDetails.CurrentUserCourses = CurrentUser.Courses.ToPagedList(coursePageNumber, coursePageSize);
            currentUserDetails.CurrentUserAddresses = CurrentUser.Addresses.ToPagedList(addressPageNumber, addressPageSize);
            currentUserDetails.CurrentUserEnrollments = CurrentUser.UserEnrollments.ToPagedList(enrollmentPageNumber, enrollmentPageSize);

            return View(currentUserDetails);
        }

        public ActionResult EditUser()
        {
            EditUserDetails userdetails = new EditUserDetails();
            userdetails.Email = CurrentUser.Email;
            userdetails.FirstName = CurrentUser.FirstName;
            userdetails.LastName = CurrentUser.LastName;
            userdetails.Birthday = CurrentUser.Birthday;
            userdetails.Gender = CurrentUser.Gender;
            userdetails.PhoneNumber = CurrentUser.PhoneNumber;

            return View(userdetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserDetails userdetails)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(CurrentUser.Email, userdetails.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid password.");
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


        public ActionResult EditPassword()
        {
            EditUserPassword userdetails = new EditUserPassword();

            return View(userdetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPassword(EditUserPassword userdetails)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(CurrentUser.Email, userdetails.OldPassword);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid password.");
                }
                else
                {
                    await UserManager.ChangePasswordAsync(user.Id, userdetails.OldPassword,userdetails.NewPassword);
                    return RedirectToAction("Index");
                }
            }
            return View(userdetails);
        }

        public JsonResult ValidateEmail(string Email)
        {
            var user = UserManager.FindByName(Email);

            if (user == null)
            {
                // new e-mail entered
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if(CurrentUser.Email.CompareTo(Email) == 0)
            {
                // same e-mail as before
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Den intastede e-mail er allerede i brug.", JsonRequestBehavior.AllowGet);
            }
        }
    }
}