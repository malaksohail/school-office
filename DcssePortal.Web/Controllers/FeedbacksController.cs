using DcssePortal.Data;
using DcssePortal.Model;
using DcssePortal.Web.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class FeedbacksController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Feedbacks
    [Authorize(Roles = "Faculty,Student")]
    public ActionResult Index()
    {
      List<Feedback> list;
      if (User.IsInRole("Faculty"))
      {

        var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Feedbacks.Where(x => x.Course.Faculty.ID == faculty.ID).ToList();
        //list = Faculty();
      }
      else
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Enrollments.Where(x=>x.Student.ID==student.ID).Select(x=>x.Course.Feedbacks.FirstOrDefault()).ToList();
      }
      return View(list);
    }

    [Authorize(Roles = "Faculty,Student")]
    public ActionResult Course(int id)
    {
      List<Feedback> list;
      if (User.IsInRole("Faculty"))
      {

        var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Feedbacks.Where(x => x.Course.Faculty.ID == faculty.ID && x.Course.ID==id).ToList();
        //list = Faculty();
      }
      else
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Enrollments.Where(x => x.Student.ID == student.ID && x.Course.ID == id).Select(x => x.Course.Feedbacks).ToList()[0];
      }
      return View(list);
    }

    // GET: Feedbacks/Details/5

    //[Authorize(Roles = "Student")]
    [Authorize(Roles = "Faculty,Student")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Feedback feedback = db.Feedbacks.Find(id);
      if (feedback == null)
      {
        return HttpNotFound();
      }
      return View(feedback);
    }

    // GET: Feedbacks/Create
    [Authorize(Roles = "Faculty")]
    public ActionResult Create()
    {
      var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
      ViewBag.Courses = db.Courses.Where(x => x.Faculty.ID == faculty.ID).ToList();
      return View();
    }

    // POST: Feedbacks/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult Create(FeedbackViewModel viewModel)
    {
      Feedback feedback = new Feedback();
      if (ModelState.IsValid)
      {
        feedback.Date = DateTime.Now;
        feedback.Title = viewModel.Title;
        var filePath = uploadFile(viewModel.file);
        if (filePath != "")
        {
          feedback.FileURL = filePath;
          feedback.Course = db.Courses.FirstOrDefault(x => x.ID == viewModel.Course);
          if (feedback.Course == null) throw new Exception("Could not find course");
          var facultyId = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email).ID;
          var courseId = Convert.ToInt32(Request.Form["Course"]);
          if (!db.Courses.Any(x => x.ID == courseId && x.Faculty.ID == facultyId))
            throw new Exception("Invalid Enrollment");
          else
          {
            //feedback.Enrollment = db.Enrollments.FirstOrDefault(x => x.Course.ID == courseId && x.Student.ID == studentId);
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
            return RedirectToAction("Index");
          }
        }
      }
      var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
      ViewBag.Courses = db.Courses.Where(x => x.Faculty.ID == faculty.ID).ToList();

      return View(feedback);
    }


    [Authorize(Roles = "Faculty,Student")]
    public FileResult Download(int id)
    {
      var file = db.Feedbacks.FirstOrDefault(x => x.ID == id);
      if (file == null)
        throw new Exception("Course Scheme not found.");
      byte[] fileBytes = System.IO.File.ReadAllBytes(file.FileURL);
      string fileName = "feedback.pdf";
      return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
    }




    // GET: Feedbacks/Edit/5
    //[Authorize(Roles = "Faculty")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Feedback feedback = db.Feedbacks.Find(id);

      if (feedback == null)
      {
        return HttpNotFound();
      }
      var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
      ViewBag.Courses = db.Courses.Where(x => x.Faculty.ID == faculty.ID).Select(x => new SelectListItem { Text = x.CourseCode, Value = x.ID.ToString() });

      FeedbackViewModel feedbackViewModel = new FeedbackViewModel()
      {
        ID = feedback.ID,
        Title = feedback.Title,
        Course = feedback.Course.ID
      };
      return View(feedbackViewModel);
    }

    // POST: Feedbacks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Faculty")]
    public ActionResult Edit(FeedbackViewModel viewModel)
    {
      Feedback feedback;// = new Feedback();
      if (ModelState.IsValid)
      {
        feedback = db.Feedbacks.Find(viewModel.ID);
        if (feedback == null) throw new ObjectNotFoundException("Feedback not found.");
        feedback.Date = DateTime.Now;
        feedback.Title = viewModel.Title;
        feedback.Course = db.Courses.FirstOrDefault(x => x.ID == viewModel.Course);
        if (feedback.Course == null) throw new Exception("Could not find course");
        var facultyId = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email).ID;
        var courseId = Convert.ToInt32(Request.Form["Course"]);
        if (!db.Courses.Any(x => x.ID == courseId && x.Faculty.ID == facultyId))
          throw new Exception("Invalid Enrollment");
        else
        {
          //feedback.Enrollment = db.Enrollments.FirstOrDefault(x => x.Course.ID == courseId && x.Student.ID == studentId);
          db.Entry(feedback).State = EntityState.Modified;
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      ViewBag.Courses = db.Courses.ToList();
      ViewBag.Students = db.Students.ToList();
      return View(viewModel);
    }

    // GET: Feedbacks/Delete/5
    //[Authorize(Roles = "Faculty")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Feedback feedback = db.Feedbacks.Find(id);
      if (feedback == null)
      {
        return HttpNotFound();
      }
      return View(feedback);
    }

    // POST: Feedbacks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    //[Authorize(Roles = "Faculty")]
    public ActionResult DeleteConfirmed(int id)
    {
      Feedback feedback = db.Feedbacks.Find(id);
      db.Feedbacks.Remove(feedback);
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
    private string uploadFile(HttpPostedFileBase file)
    {
      try
      {
        if (file.ContentLength > 0)
        {
          string _FileName = Path.GetFileName(file.FileName);
          string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), DateTime.Now.ToLongDateString() + "_feedback" + _FileName);

          file.SaveAs(_path);

          return _path;
        }
      }
      catch (Exception e) { }
      return "";
    }
  }
}
