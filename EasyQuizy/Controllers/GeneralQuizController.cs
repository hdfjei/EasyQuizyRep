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

            return RedirectToAction("CreateQuestion", new { id = generalQuiz.Id });
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
        [HttpGet]
        public ActionResult CreateQuestion(int id)
        {
            Question question = new Question() { GeneralQuizId = id };
            return View(question);
        }
        [HttpPost]
        public ActionResult CreateQuestion(Question question, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    question.ImageMimeType = image.ContentType;
                    question.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(question.ImageData, 0, image.ContentLength);
                }
                db.Questions.Add(question);
                db.SaveChanges();
                
                return RedirectToAction("CreateQuestion", new { id = question.GeneralQuizId });
            }
            else
            {
                return View(question);
            }
        }
        public FileContentResult GetImage(int questionId)
        {
            Question question = db.Questions.FirstOrDefault(q => q.Id == questionId);

            if (question != null)
            {
                return File(question.ImageData, question.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ActionResult ShowGeneralQuiz(int id)
        {
            var questions = db.Questions.Where(q => q.GeneralQuizId == id).Include(q => q.Answers);
            ViewBag.GeneralQuiz = db.GeneralQuizes.Where(qg => qg.Id == id).First().Name;
            return View(questions.ToList());
        }
        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            Question question = db.Questions.Find(id);
            if (question != null)
            {
                return View(question);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}