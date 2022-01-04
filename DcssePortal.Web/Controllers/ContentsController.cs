using DcssePortal.Data;
using DcssePortal.Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class ContentsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Contents
    [Authorize(Roles = "Student, Faculty")]
    public ActionResult Index()
    {
      List<Content> list;
      if (User.IsInRole("Faculty"))
      {

        var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Contents.Where(x => x.Course.Faculty.ID == faculty.ID).ToList();
        //list = Faculty();
      }
      else
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Enrollments.Where(x => x.Student.ID == student.ID).Select(x => x.Course.Contents.FirstOrDefault()).ToList();
      }
      return View(list);
    }


    [Authorize(Roles = "Student, Faculty")]
    public ActionResult Course(int id)
    {
      List<Content> list;
      if (User.IsInRole("Faculty"))
      {

        var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Contents.Where(x => x.Course.Faculty.ID == faculty.ID && id==x.Course.ID).ToList();
        //list = Faculty();
      }
      else
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Enrollments.Where(x => x.Student.ID == student.ID && id == x.Course.ID).Select(x => x.Course.Contents).ToList()[0];
      }
      return View(list);
    }



    // GET: Contents/Details/5
    [Authorize(Roles = "Student, Faculty")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Content content = db.Contents.Find(id);
      if (content == null)
      {
        return HttpNotFound();
      }
      return View(content);
    }

    // GET: Contents/Create
    [Authorize(Roles = "Faculty")]
    public ActionResult Create()
    {
      var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
      ViewBag.Courses =db.Courses.Where(x => x.Faculty.ID == faculty.ID).Select(x => new SelectListItem { Text = x.CourseCode, Value = x.ID.ToString() });
      return View();
    }

    // POST: Contents/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult Create(HttpPostedFileBase ContentUrl, int CourseId)
    {
      Content content = new Content();
      TryUpdateModel(content);
      if (ModelState.IsValid)
      {
        content.Date = DateTime.Now;
        var filePath = uploadFile(ContentUrl);
        if (filePath != "")
        {
          content.ContentUrl = filePath;
          content.Course = db.Courses.FirstOrDefault(x => x.ID == CourseId);
          db.Contents.Add(content);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }

      var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
      ViewBag.Courses = db.Courses.Where(x => x.Faculty.ID == faculty.ID).Select(x => new SelectListItem { Text = x.CourseCode, Value = x.ID.ToString() });
      return View(content);
    }

    public FileResult Download(int id)
    {
      var file = db.Contents.FirstOrDefault(x => x.ID == id);
      if (file == null)
        throw new Exception("Course Scheme not found.");
      byte[] fileBytes = System.IO.File.ReadAllBytes(file.ContentUrl);
      string fileName = "courseContent.pdf";
      return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
    }



    // GET: Contents/Edit/5
    [Authorize(Roles = "Faculty")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Content content = db.Contents.Find(id);
      if (content == null)
      {
        return HttpNotFound();
      }
      return View(content);
    }

    // POST: Contents/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult Edit(Content content)
    {
      if (ModelState.IsValid)
      {
        content.Date = DateTime.Now;
        db.Entry(content).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(content);
    }

    // GET: Contents/Delete/5
    [Authorize(Roles = "Faculty")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Content content = db.Contents.Find(id);
      if (content == null)
      {
        return HttpNotFound();
      }
      return View(content);
    }

    // POST: Contents/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult DeleteConfirmed(int id)
    {
      Content content = db.Contents.Find(id);
      db.Contents.Remove(content);
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
          string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), DateTime.Now.ToLongDateString()+ "_Contents" + _FileName);
         
          file.SaveAs(_path);

          return _path;
        }
      }
      catch(Exception e) { }
      return "";
    }
  }
}

