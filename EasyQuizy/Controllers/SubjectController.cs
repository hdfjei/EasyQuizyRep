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
        // GET: Subject
        QuizContext db = new QuizContext();

        public ActionResult Index()
        {
            var subjects = db.Subjects;
            return View(subjects.ToList());
        }
        [HttpGet]
        public ActionResult CreateSubject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateSubject(Subject subject)
        {
            db.Subjects.Add(subject);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}