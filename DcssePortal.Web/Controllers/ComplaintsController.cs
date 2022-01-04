using DcssePortal.Data;
using DcssePortal.Model;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class ComplaintsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Complaints
    [Authorize(Roles = "Admin,Student")]
    public ActionResult Index()
    {
      List<Complaints> list;
      if (User.IsInRole("Student"))
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Complaints.Where(x => x.Student.ID == student.ID).ToList();
      }
      else
      {
        list = db.Complaints.ToList();
      }
      return View(list);
    }

    // GET: Complaints/Details/5
    [Authorize(Roles = "Admin,Student")]
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Complaints complaints = db.Complaints.Find(id);
      if (complaints == null)
      {
        return HttpNotFound();
      }
      return View(complaints);
    }

    // GET: Complaints/Create
    [Authorize(Roles = "Student")]
    public ActionResult Create()
    {
      return View();
    }

    // POST: Complaints/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Authorize(Roles = "Student")]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ID,Title,Detail")] Complaints complaints)
    {
      complaints.Student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => HttpContext.User.Identity.Name == y.UserName).Email);
      if (complaints.Student == null)
      {
        ModelState.AddModelError("NullStudent", "Could not find student");
      }
      if (ModelState.IsValid)
      {
        db.Complaints.Add(complaints);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(complaints);
    }

    // GET: Complaints/Edit/5
    [Authorize(Roles = "Student")]
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Complaints complaints = db.Complaints.Find(id);
      if (complaints == null)
      {
        return HttpNotFound();
      }
      return View(complaints);
    }

    // POST: Complaints/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Student")]
    public ActionResult Edit([Bind(Include = "ID,Title,Detail")] Complaints complaints)
    {
      if (ModelState.IsValid)
      {
        db.Entry(complaints).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(complaints);
    }

    // GET: Complaints/Delete/5
    [Authorize(Roles = "Student")]
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Complaints complaints = db.Complaints.Find(id);
      if (complaints == null)
      {
        return HttpNotFound();
      }
      return View(complaints);
    }

    // POST: Complaints/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Student")]
    public ActionResult DeleteConfirmed(int id)
    {
      Complaints complaints = db.Complaints.Find(id);
      db.Complaints.Remove(complaints);
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
