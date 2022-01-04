using DcssePortal.Data;
using DcssePortal.Model;
using Microsoft.Ajax.Utilities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  [Authorize(Roles = "Admin")]
  public class StudentsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Students
    public ActionResult Index()
    {
      return View(db.Students.ToList());
    }

    // GET: Students/Details/5
    [Authorize(Roles = "Admin,Student")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Student student = db.Students.Find(id);
      if (student == null)
      {
        return HttpNotFound();
      }
      return View(student);
    }

    // GET: Students/Create
    public ActionResult Create()
    {
      return RedirectToAction("register", "account");

    }

    // GET: Students/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Student student = db.Students.Find(id);
      if (student == null)
      {
        return HttpNotFound();
      }
      return View(student);
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Student student)
    {
      if (db.Students.Where(x => x.RegNo == student.RegNo && x.ID!=student.ID).ToList().Count > 0)
        ModelState.AddModelError("Duplicate Reg Number", "Duplicate Registration number");
      if (student.DOB >= DateTime.Now.AddYears(-16))
        ModelState.AddModelError("User under-age", "Minimum age required for BS is 16 years");
      if (ModelState.IsValid)
      {

        db.Entry(student).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");

      }
      return View(student);
    }

    // GET: Students/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Student student = db.Students.Find(id);
      if (student == null)
      {


        return HttpNotFound();
      }
      return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Student student = db.Students.Find(id);
      db.Students.Remove(student);
      db.Users.Remove(db.Users.FirstOrDefault(x => x.Email == student.Email));
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
