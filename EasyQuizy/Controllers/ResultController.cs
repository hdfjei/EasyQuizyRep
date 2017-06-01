using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EasyQuizy.Models;
using EasyQuizy.Models.ViewModels;

namespace EasyQuizy.Controllers
{
    public class ResultController : Controller
    {
        QuizContext db = new QuizContext();
        public ActionResult Index(int? formedQuiz, int? group)
        {
            IQueryable<Result> results = null;

            if (formedQuiz != 0 && formedQuiz != null)
            {
                results = db.Results.Where(r => r.FormedQuizId == formedQuiz).Include(r => r.Student);
                if (group != 0 && group != null)
                {
                    var students = db.Students.Where(s => s.GroupId == group);
                    results = results.Where(r => students.Contains(r.Student));
                }
            }

            var formeqQuizes = db.FormedQuizes;
            var groups = db.Groups;
            //var formeqQuizes = db.FormedQuizes.Join(db.Results, f => f.Id, r => r.FormedQuizId, (f,r) => new
            //{
            //    Id = f.Id,
            //    Name = f.Name
            //}).ToList().Select(x=> new FormedQuiz { Id = x.Id, Name = x.Name }).GroupBy(x => x.Id).Select(g => g.First());

            //var groups = db.Groups.Join(db.Results, g => g.Id, r => r.StudentId, (g, r) =>
            //new { g, r }).Join(db.Groups, s => s.r.StudentId, ss => ss.Id, (ss, s) => new
            //{
            //    Id = ss.g.Id,
            //    Name = ss.g.Name
            //}).ToList().Select(x=> new Group { Id = x.Id, Name = x.Name }).ToList();

            ResultListViewModel rlvm = new ResultListViewModel
            {
                FormedQuizes = new SelectList(formeqQuizes, "Id", "Name"),
                Results = results,
                Groups = new SelectList(groups, "Id", "Name")
            };


            return View(rlvm);
        }
        [HttpGet]
        public ActionResult Statistics()
        {
            var formeqQuizes = db.FormedQuizes;
            var groups = db.Groups;

            ChartViewModel cvm = new ChartViewModel
            {
                FormedQuizes = new SelectList(formeqQuizes, "Id", "Name"),
                Groups = new SelectList(groups, "Id", "Name")
            };
            return View(cvm);
        }
        [HttpPost]
        public ActionResult Statistics(int? formedQuiz, int? group)
        {
            IQueryable<Result> results = null;

            if (formedQuiz != 0 && formedQuiz != null)
            {
                results = db.Results.Where(r => r.FormedQuizId == formedQuiz).Include(r => r.Student);
                if (group != 0 && group != null)
                {
                    var students = db.Students.Where(s => s.GroupId == group);
                    results = results.Where(r => students.Contains(r.Student));
                }
            }

            List<string> names = new List<string>();
            List<double> resultsInPercent = new List<double>();
            foreach (var item in results)
            {
                names.Add($"{item.Student.LastName} {item.Student.FirstName} {item.Student.MiddleName}");
                resultsInPercent.Add(item.ResultInPercent);
            }
            ChartViewModel cvm = new ChartViewModel
            {
                Names = names,
                Results = resultsInPercent
            };
            TempData["cvm"] = cvm;
            return RedirectToAction("ShowChart");
        }
        public ActionResult ShowChart()
        {
            ChartViewModel cvm = (ChartViewModel)TempData["cvm"];
            return View(cvm);
        }
    }
}