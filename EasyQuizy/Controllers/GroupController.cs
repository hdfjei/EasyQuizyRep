using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyQuizy.Models;
using System.Data.Entity;

namespace EasyQuizy.Controllers
{
    public class GroupController : Controller
    {
        QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            var groups = db.Groups;
            return View(groups.ToList());
        }
        [HttpGet]
        public ActionResult CreateGroup()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateGroup(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteGroup(int id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult ShowStudents(int id)
        {
            var students = db.Students.Where(c => c.Group.Id == id).Include(c => c.Group);
            ViewBag.GroupId = id;
            ViewBag.Group = db.Groups.Where(s => s.Id == id).First().Name;
            return View(students.ToList());
        }
        [HttpGet]
        public ActionResult CreateStudent(int id)
        {
            Student student = new Student() { GroupId = id };
            return PartialView(student);
        }
        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();

            return RedirectToAction("ShowStudents", new { id = student.GroupId });
        }
    }
}