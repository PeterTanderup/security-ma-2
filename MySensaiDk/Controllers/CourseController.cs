using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySensaiDk.Infrastructure;
using MySensaiDk.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace MySensaiDk.Controllers
{
    [Authorize(Roles = "Users")]
    public class CourseController : Controller
    {
        private MySensaiDkDbContext db = new MySensaiDkDbContext();

        // GET: Admin/AdminCourse
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var courses = db.Courses.Include(c => c.Address).Include(c => c.User).ToList();
            return View(courses.ToPagedList(pageNumber, pageSize));
        }

        // GET: Course/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "AddressName");
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titel, Description, StartTime, EndTime, ResponseDate, MaxSpots, AddressId")] CourseModel cm)
        {
            Course course = new Course();

            if (ModelState.IsValid)
            {
                course.Titel = cm.Titel;
                course.Description = cm.Description;
                course.StartTime = cm.StartTime;
                course.EndTime = cm.EndTime;
                course.ResponseDate = cm.ResponseDate;
                course.MaxSpots = cm.MaxSpots;
                course.AddressId = course.AddressId;
                course.TeacherID = User.Identity.GetUserId();

                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "AddressName", course.AddressId);
            return View(cm);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "AddressName", course.AddressId);
            return View(new CourseModel(course));
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, [Bind(Include = "Titel, Description, StartTime, EndTime, ResponseDate, MaxSpots, AddressId, TeacherID")] CourseModel cm)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = new Course();

            if (ModelState.IsValid)
            {

                course.CourseId = id.Value;
                course.Titel = cm.Titel;
                course.Description = cm.Description;
                course.StartTime = cm.StartTime;
                course.EndTime = cm.EndTime;
                course.ResponseDate = cm.ResponseDate;
                course.MaxSpots = cm.MaxSpots;
                course.AddressId = course.AddressId;
                course.TeacherID = cm.TeacherID;

                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "AddressId", "AddressName", course.AddressId);
            return View(cm);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
