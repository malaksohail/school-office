using DcssePortal.Data;
using DcssePortal.Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DcssePortal.Web.Controllers
{
  public class CoursesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Courses
    [Authorize(Roles = "Student, Faculty")]
    public ActionResult Index()
    {
      List<Course> list;
      if (User.IsInRole("Faculty"))
      {

        var faculty = db.Faculties.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Courses.Include(x=>x.Feedbacks).Where(x => x.Faculty.ID == faculty.ID).ToList();
        //list = Faculty();
      }
      else
      {
        var student = db.Students.FirstOrDefault(x => x.Email == db.Users.FirstOrDefault(y => y.UserName == User.Identity.Name).Email);
        list = db.Enrollments.Where(x => x.Student.ID == student.ID).Select(x => x.Course).ToList();
      }
      return View(list);
    }

    // GET: Courses/Details/5
    [Authorize(Roles = "Student, Faculty")]
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

    // GET: Courses/Create
    [Authorize(Roles = "Faculty")]
    public ActionResult Create()
    {
      return View();
    }

    // POST: Courses/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult Create(Course course)
    {
      if (ModelState.IsValid)
      {
        var faculties = from faculty in db.Faculties
                        join user in db.Users on faculty.Email equals user.Email
                        select faculty;
        if (faculties.Count() == 0)
        {
          ModelState.AddModelError("NullFaculty","Faculty not found");
        }
        course.Faculty = faculties.First();
        if (db.Courses.Any(x => x.CourseCode == course.CourseCode && course.Faculty.ID == x.Faculty.ID))
        {
          ModelState.AddModelError("DuplicateCourse", "Course already assigned");
        }
        if (course.CreditHour == 1 || course.CreditHour == 3 || course.CreditHour == 4 || course.CreditHour == 6)
        {
          db.Courses.Add(course);

          db.SaveChanges();
          return RedirectToAction("Index");
        }
        else ModelState.AddModelError("InvalidCreditHours", "Invalid credit hours");
      }

      return View(course);
    }

    // GET: Courses/Edit/5
    [Authorize(Roles = "Faculty")]
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
      return View(course);
    }

    // POST: Courses/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult Edit(Course course)
    {
      if (ModelState.IsValid)
      {
        db.Entry(course).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(course);
    }

    // GET: Courses/Delete/5
    [Authorize(Roles = "Faculty")]
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

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Faculty")]
    public ActionResult DeleteConfirmed(int id)
    {
      Course course = db.Courses.Find(id);
      db.Courses.Remove(course);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Student")]
    public ActionResult Join()
    {
      return View();
    }


    [HttpPost]
    [Authorize(Roles = "Student")]
    public ActionResult Join(FormCollection forms)
    {
      var instructorId = Convert.ToInt32(forms["InstructureId"]);
      var courseCode = forms["courseCode"];
      var secretCode = forms["secretCode"].Trim();
      if (!db.Faculties.Any(x => x.ID == instructorId))
        ModelState.AddModelError("NullFaculty", "could not find instructor");
      if (ModelState.IsValid)
      {
        var course = db.Courses.FirstOrDefault(x => x.CourseCode == courseCode && x.Faculty.ID == instructorId);
        if (course == null)
        {
          ModelState.AddModelError("NullCourse", "could not find course");
        }
        else
        {
          var currentStudents = from std in db.Students
                                join user in db.Users on std.Email equals user.Email
                                select std;
          if (currentStudents.Count() == 0)
          {
            ModelState.AddModelError("NullStudent", "could not find student");
          }
          else
          {
            var currentStudent = currentStudents.FirstOrDefault();
            
            if (course.SecretCode == secretCode)
            {
              if (db.Enrollments.Any(x => x.Course.ID == course.ID && x.Student.ID == currentStudent.ID))
              {
                ModelState.AddModelError("DuplicateEnrollment", "Student already enrolled in given course");
              }
              else
              {
                course.Enrollments.Add(new Enrollment { Course = course, Student = currentStudent });
                db.SaveChanges();
              }
            }
            else
            {
              ModelState.AddModelError("invalid key", "invalid secret key");
            }
          }
        }
      }
      return View();
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
