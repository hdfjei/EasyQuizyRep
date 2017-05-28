using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using EasyQuizy.Models;

namespace EasyQuizy.Controllers
{
    public class SubjectController : Controller
    {
        QuizContext db = new QuizContext();

        public ActionResult Index()
        {
            var subjects = db.Subjects;
            return View(subjects.ToList());
        }
        //Работа с предметами
        [HttpGet]
        public ActionResult CreateSubject()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateSubject(Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteSubject(int id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }

            db.Subjects.Remove(subject);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //Работа с категориями
        public ActionResult ShowCategories(int id)
        {
            var categories = db.Categories.Where(c => c.Subject.Id == id).Include(c=>c.Subject);
            ViewBag.SubjectId = id;
            ViewBag.Subject = db.Subjects.Where(s => s.Id == id).First().Name;
            return View(categories.ToList());
        }
        public ActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();
            //возвращает список предметов, было бы неплохо исправить
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateCategory(int id)
        {
            Category category = new Category() { SubjectId = id };
            return PartialView(category);
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("ShowCategories", new { id = category.SubjectId });
        }
    }
}