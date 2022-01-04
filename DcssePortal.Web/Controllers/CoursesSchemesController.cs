using DcssePortal.Data;
using DcssePortal.Model;
using DcssePortal.Web.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  [Authorize]
  public class CoursesSchemesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: CoursesSchemes
    public ActionResult Index()
    {
      return View(db.CoursesSchemes.ToList());
    }

    // GET: CoursesSchemes/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
      if (coursesScheme == null)
      {
        return HttpNotFound();
      }
      return View(coursesScheme);
    }

    // GET: CoursesSchemes/Create
    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      return View();
    }

    // POST: CoursesSchemes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult Create(string Title, string Department, HttpPostedFileBase file)
    {
      var coursesScheme = new CoursesScheme();
      TryUpdateModel(coursesScheme);
      if (ModelState.IsValid)
      {
        coursesScheme.Date = DateTime.Now;
        var filePath = uploadFile(file);
        if(filePath!="")
        {
          coursesScheme.FileUrl = filePath;
          db.CoursesSchemes.Add(coursesScheme);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError("File upload error", "Failed to upload file.");
        }
      }
      CourseSchemeViewModel viewModel = new CourseSchemeViewModel();
      TryUpdateModel(viewModel);
      return View(viewModel);
    }


    public FileResult Download(int id)
    {
      var file = db.CoursesSchemes.FirstOrDefault(x => x.ID == id);
      if (file== null)
        throw new Exception("Course Scheme not found.");
      byte[] fileBytes = System.IO.File.ReadAllBytes(file.FileUrl);
      string fileName = "courseScheme.pdf";
      return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
    }


    // GET: CoursesSchemes/Delete/5
    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
      if (coursesScheme == null)
      {
        return HttpNotFound();
      }
      return View(coursesScheme);
    }

    // POST: CoursesSchemes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult DeleteConfirmed(int id)
    {
      CoursesScheme coursesScheme = db.CoursesSchemes.Find(id);
      db.CoursesSchemes.Remove(coursesScheme);
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
          string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), DateTime.Now.ToLongDateString() + _FileName);
          file.SaveAs(_path);

          return _path;
        }
      }
      catch {  }
      return "";
    }
  }
}
