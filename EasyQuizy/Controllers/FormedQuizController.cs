using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyQuizy.Models;
using EasyQuizy.Models.ViewModels;
using System.Data.Entity;
using EasyQuizy.Models.FormedQuizLogic;

namespace EasyQuizy.Controllers
{
    public class FormedQuizController : Controller
    {
        QuizContext db = new QuizContext();
        [HttpGet]
        public ActionResult Index(int? subject, int? category)
        {
            IQueryable<GeneralQuiz> generalQuizes = db.GeneralQuizes.Include(gq => gq.Subject).Include(gq => gq.Category);
            if (subject != 0 && subject != null)
            {
                generalQuizes = generalQuizes.Where(gq => gq.SubjectId == subject);
                if (category != null && category != 0)
                {
                    generalQuizes = generalQuizes.Where(gq => gq.SubjectId == subject && gq.CategoryId == category);
                }
            }
            List<Subject> subjects = db.Subjects.ToList();
            subjects.Insert(0, new Subject { Id = 0, Name = "Все" });
            List<Category> categories = db.Categories.Where(c => c.SubjectId == subject).ToList();
            categories.Insert(0, new Category { Id = 0, Name = "Все" });

            QuizListViewModel qlvm = new QuizListViewModel
            {
                GeneralQuizes = generalQuizes.ToList(),
                Subjects = new SelectList(subjects, "Id", "Name"),
                Categories = new SelectList(categories, "Id", "Name")
                
            };
            return View(qlvm);
        }
        [HttpPost]
        public ActionResult Index(QuizListViewModel qlvm)
        {

            FormedQuizMaker fqm = new FormedQuizMaker(qlvm.Quizes, qlvm.IsChoosen, qlvm.VariantsNumber, qlvm.QuestionsNumber);
            List<Question>[] temp = fqm.FormQuizes();

            for (int i = 0; i < qlvm.VariantsNumber; i++)
            {
                FormedQuiz fq = new FormedQuiz
                {
                    Name = $"{qlvm.FormedQuizName}Variant{i + 1}",
                    Questions = temp[i],
                    VariantsNumber = qlvm.VariantsNumber,
                    QuestionsNumber = qlvm.QuestionsNumber,
                    GenerationType = $"type1"
                };
                db.FormedQuizes.Add(fq);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}