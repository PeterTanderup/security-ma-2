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

namespace MySensaiDk.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}