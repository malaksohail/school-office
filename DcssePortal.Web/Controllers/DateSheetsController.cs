using DcssePortal.Data;
using DcssePortal.Model;
using DcssePortal.Web.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  [Authorize]
  public class DateSheetsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: DateSheets
    public ActionResult Index()
    {

      return View(db.DateSheets.ToList());
    }
    //public ActionResult Search()
    //{

    //    return View();
    //}

    // GET: DateSheets/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DateSheet dateSheet = db.DateSheets.Find(id);
      if (dateSheet == null)
      {
        return HttpNotFound();
      }
      return View(dateSheet);
    }

    // GET: DateSheets/Create
    [Authorize(Roles = "Admin")]
    public ActionResult Create()
    {
      return View();
    }

    // POST: DateSheets/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult Create(HttpPostedFileBase File)
    {
      DateSheet dateSheet = new DateSheet();
      TryUpdateModel(dateSheet);

      if (ModelState.IsValid)
      {

        dateSheet.Date = DateTime.Now;
        var filePath = uploadFile(File);
        if (filePath != "")
        {
          dateSheet.ContentUrl = filePath;
          db.DateSheets.Add(dateSheet);
          db.SaveChanges();
          return RedirectToAction("Index");
        }
      }
      DateSheetViewModel viewModel = new DateSheetViewModel();
      TryUpdateModel(viewModel);
      return View(viewModel);
    }


    public FileResult Download(int id)
    {
      var file = db.DateSheets.FirstOrDefault(x => x.ID == id);
      if (file == null)
        throw new Exception("Datesheet not found.");
      byte[] fileBytes = System.IO.File.ReadAllBytes(file.ContentUrl);
      string fileName = "datesheet.pdf";
      return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
    }



    // GET: DateSheets/Edit/5

    [Authorize(Roles = "Admin")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DateSheet dateSheet = db.DateSheets.Find(id);
      if (dateSheet == null)
      {
        return HttpNotFound();
      }
      return View(dateSheet);
    }

    // POST: DateSheets/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]

    [Authorize(Roles = "Admin")]
    public ActionResult Edit([Bind(Include = "ID,Title,Content,Department")] DateSheet dateSheet)
    {
      if (ModelState.IsValid)
      {
        dateSheet.Date = DateTime.Now;
        db.Entry(dateSheet).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(dateSheet);
    }

    // GET: DateSheets/Delete/5

    [Authorize(Roles = "Admin")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      DateSheet dateSheet = db.DateSheets.Find(id);
      if (dateSheet == null)
      {
        return HttpNotFound();
      }
      return View(dateSheet);
    }

    // POST: DateSheets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]

    [Authorize(Roles = "Admin")]
    public ActionResult DeleteConfirmed(int id)
    {
      DateSheet dateSheet = db.DateSheets.Find(id);
      db.DateSheets.Remove(dateSheet);
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
          string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), DateTime.Now.ToLongDateString() + "_Datesheet_" + _FileName);
          file.SaveAs(_path);

          return _path;
        }
      }
      catch { }
      return "";
    }
  }
}
