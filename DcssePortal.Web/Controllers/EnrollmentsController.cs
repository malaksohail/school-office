using DcssePortal.Data;
using DcssePortal.Model;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class EnrollmentsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Enrollments
    [Authorize(Roles ="Admin,Student")]
    public ActionResult Index()
    {
      List<Enrollment> list;
      if (User.IsInRole("Admin"))
      {
        list = db.Enrollments.ToList();
      }
      else
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Enrollments.Where(x => x.Student.ID == student.ID).ToList();
      }
      return View(list);
    }

    // GET: Enrollments/Details/5
    [Authorize(Roles = "Admin,Student")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Enrollment enrollment = db.Enrollments.Find(id);
      if (enrollment == null)
      {
        return HttpNotFound();
      }
      return View(enrollment);
    }

    // GET: Enrollments/Create

    //[Authorize(Roles = "Student,Faculty")]
    //[Authorize(Roles = "Admin")]
    //public ActionResult Create()
    //{
    //  ViewBag.Courses = db.Courses.ToList();
    //  ViewBag.Students = db.Students.ToList();
    //  return View();
    //}

    // POST: Enrollments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    ////[Authorize(Roles = "Admin,Faculty")]
    //[Authorize(Roles = "Admin")]
    //public ActionResult Create(FormCollection forms)
    //{
    //  var enrollment = new Enrollment();
    //  if (ModelState.IsValid)
    //  {
    //    var courseId = Convert.ToInt32(Request.Form["Course"]);
    //    var studentId = Convert.ToInt32(Request.Form["Student"]);
    //    enrollment.Student = db.Students.FirstOrDefault(x => x.ID == studentId);
    //    enrollment.Course = db.Courses.FirstOrDefault(x => x.ID == courseId);
    //    if (db.Enrollments.Any(x => x.Course.ID == courseId && x.Student.ID == studentId))
    //      ModelState.AddModelError("Duplicate Enrollment", new Exception("Duplicate Enrollment"));
    //    else
    //    {
    //      db.Enrollments.Add(enrollment);
    //      db.SaveChanges();
    //      return RedirectToAction("Index");
    //    }
    //  }

    //  ViewBag.Courses = db.Courses.ToList();
    //  ViewBag.Students = db.Students.ToList();
    //  return View(enrollment);
    //}

    //// GET: Enrollments/Edit/5
    //[Authorize(Roles = "Admin")]
    //public ActionResult Edit(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Enrollment enrollment = db.Enrollments.Find(id);
    //  if (enrollment == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(enrollment);
    //}

    //// POST: Enrollments/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //[Authorize(Roles = "Faculty")]
    //public ActionResult Edit([Bind(Include = "ID")] Enrollment enrollment)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(enrollment).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  return View(enrollment);
    //}

    // GET: Enrollments/Delete/5

    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Enrollment enrollment = db.Enrollments.Find(id);
      if (enrollment == null)
      {
        return HttpNotFound();
      }
      return View(enrollment);
    }

    // POST: Enrollments/Delete/5
    [HttpPost, ActionName("Delete")]

    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Enrollment enrollment = db.Enrollments.Find(id);
      db.Enrollments.Remove(enrollment);
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
