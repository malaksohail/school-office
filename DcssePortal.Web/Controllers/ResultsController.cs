using DcssePortal.Data;
using DcssePortal.Model;
using DcssePortal.Web.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
 
  public class ResultsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Results
    [Authorize(Roles ="Admin,Student")]
    public ActionResult Index()
    {

      List<Result> list;
      if (User.IsInRole("Faculty"))
      {

        var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Results.Where(x => x.Course.Faculty.ID== faculty.ID).ToList();
        //list = Faculty();
      }
      else if (User.IsInRole("Faculty"))
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Results.Where(x => x.Student.ID == student.ID).ToList();
      }
      else
      {
        list = db.Results.ToList();
      }
      return View(list);
    }

    // GET: Results/Details/5

    [Authorize(Roles = "Admin,Student")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Result result = db.Results.Find(id);
      if (result == null)
      {
        return HttpNotFound();
      }
      return View(result);
    }

    // GET: Results/Create

    //[Authorize(Roles = "Admin")]
    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View();
    }

    // POST: Results/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Admin")]
    [Authorize(Roles = "Admin")]
    public ActionResult Create(ResultViewModel viewModel)
    {
      var result = new Result();
      if (ModelState.IsValid)
      {
        var studentId = Convert.ToInt32(Request.Form["Student"]);
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        if (!db.Enrollments.Any(x => x.Student.ID == studentId && x.Course.ID == courseId))
          ModelState.AddModelError("NullEnrollment","Enrollment doesnot exists");
        else
        {
          //result.Enrollment = db.Enrollments.FirstOrDefault(x => x.Student.ID == studentId && x.Course.ID == courseId);
          result.Student = db.Students.FirstOrDefault(x => x.ID == studentId);
          result.Course = db.Courses.FirstOrDefault(x => x.ID == courseId);
          if (result.Student == null || result.Course == null) ModelState.AddModelError("NullEnrollment","Could not find Enrollment");
          result.ExternalMarks = viewModel.External;
          result.InternalMarks = viewModel.Internal;
          db.Results.Add(result);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(viewModel);
    }

    // GET: Results/Edit/5
    [Authorize(Roles = "Admin")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Result result = db.Results.Include(x=>x.Student).Include(x=>x.Course).ToList().Find(x=>x.ID==id);
      if (result == null)
      {
        return HttpNotFound();
      }
      var viewModel = new ResultViewModel
      {
        ID = result.ID,
        Internal = result.InternalMarks,
        External = result.ExternalMarks,
        Course=result.Course.ID,
        Student=result.Student.ID
      };
      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(viewModel);
    }

    // POST: Results/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult Edit(ResultViewModel viewModel)
    {
      var result = db.Results.Find(viewModel.ID);
      if (result == null) ModelState.AddModelError("NullResult","Result not found");
      if (ModelState.IsValid)
      {
        result.InternalMarks = viewModel.Internal;
        result.ExternalMarks = viewModel.External;
        result.Course = db.Courses.Find(viewModel.Course);
        result.Student = db.Students.Find(viewModel.Student);
        if (result.Course == null || result.Student == null) ModelState.AddModelError("NullEnrollment","Could not find Enrollment");
        db.Entry(result).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(viewModel);
    }

    // GET: Results/Delete/5
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Result result = db.Results.Find(id);
      if (result == null)
      {
        return HttpNotFound();
      }
      return View(result);
    }

    // POST: Results/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult DeleteConfirmed(int id)
    {
      Result result = db.Results.Find(id);
      db.Results.Remove(result);
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

    [NonAction]
    private string _GetGrade(int obtainedMarks)
    {
      if (obtainedMarks >= 90) return "A";
      if (obtainedMarks >= 85) return "B+";
      if (obtainedMarks >= 80) return "B";
      if (obtainedMarks >= 75) return "C+";
      if (obtainedMarks >= 70) return "C";
      if (obtainedMarks >= 65) return "D+";
      if (obtainedMarks >= 60) return "D";
      return "F";

    }
  }
}
