using EasyQuizy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EasyQuizy.Controllers
{
    public class GeneralQuizController : Controller
    {
        QuizContext db = new QuizContext();
        public ActionResult Index()
        {
            var generalQuizes = db.GeneralQuizes.Include(g => g.Subject).Include(g => g.Category);
            return View(generalQuizes);
        }

        //не добавляет categoryId, если хоть раз тронуть dropdownlist, нужно разобраться, почему
        [HttpGet]
        public ActionResult CreateGeneralQuiz()
        {
            int selectedIndex = 1;
            var subjects = new SelectList(db.Subjects, "Id", "Name");
            var categories = new SelectList(db.Categories.Where(c => c.SubjectId == selectedIndex), "Id", "Name");
            ViewBag.Subjects = subjects;
            ViewBag.Categories = categories;
            return View();
        }
        public ActionResult GetCategoriesBySubject(int id)
        {
            return PartialView(db.Categories.Where(c => c.SubjectId == id).ToList());
        }
        [HttpPost]
        public ActionResult CreateGeneralQuiz(GeneralQuiz generalQuiz)
        {
            db.GeneralQuizes.Add(generalQuiz);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteGeneralQuiz(int id)
        {
            GeneralQuiz generalQuiz = db.GeneralQuizes.Find(id);
            if (generalQuiz == null)
            {
                return HttpNotFound();
            }

            db.GeneralQuizes.Remove(generalQuiz);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}