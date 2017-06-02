using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyQuizy.Models;
using EasyQuizy.Models.ViewModels;
using System.Data.Entity;
using EasyQuizy.Models.FormedQuizLogic;
using Novacode;
using System.IO;
using System.Drawing;

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
            List<Question>[] temp = fqm.FormQuizes(GenerationType.Two);

            for (int i = 0; i < qlvm.VariantsNumber; i++)
            {
                FormedQuiz fq = new FormedQuiz
                {
                    Name = $"{qlvm.FormedQuizName}Variant{i + 1}",
                    VariantsNumber = qlvm.VariantsNumber,
                    QuestionsNumber = qlvm.QuestionsNumber,
                    GenerationType = $"{GenerationType.Two.ToString()}"
                };
                db.FormedQuizes.Add(fq);
            }
            db.SaveChanges();

            MemoryStream stream = new MemoryStream();
            DocX doc = DocX.Create(stream);

            for (int i = 0; i < temp.Length; i++)
            {
                FillDocument(ref doc, temp[i], i + 1);
            }
            
            return File(stream.ToArray(), "application/octet-stream", $"{qlvm.FormedQuizName}.docx");
        }
        private void FillDocument(ref DocX doc, List<Question> questions, int variant)
        {
            Paragraph header = doc.InsertParagraph();
            header.SetLineSpacing(LineSpacingType.After, 1.5f);
            header.Append($"Вариант {variant}").Font(new FontFamily("Times New Roman")).FontSize(20).Color(Color.Black);
            for (int i = 0; i < questions.Count; i++)
            {
                Paragraph question = doc.InsertParagraph();
                question.SetLineSpacing(LineSpacingType.After, 1.3f);
                question.Append($"{i+1}. {questions[i].Content}").Font(new FontFamily("Times New Roman")).FontSize(14).Color(Color.Black);

                Stream str = GetImage(questions[i].Id);
                if (str != null)
                {
                    Novacode.Image img = doc.AddImage(str);
                    Picture picture = img.CreatePicture();
                    question.AppendLine("").AppendPicture(picture);
                }
                    
                Paragraph answer = doc.InsertParagraph();
                for (int j = 0; j < questions[i].Answers.Count; j++)
                {
                    answer.SetLineSpacing(LineSpacingType.After, 1.3f);
                    answer.Append($"{j+1}) {questions[i].Answers.ToList()[j].Content}\t").Font(new FontFamily("Times New Roman")).FontSize(14).Color(Color.Black);
                }
            }
            Paragraph footer = doc.InsertParagraph();
            footer.InsertPageBreakAfterSelf();      
            doc.Save();
        }
        private MemoryStream GetImage(int questionId)
        {
            Question question = db.Questions.FirstOrDefault(q => q.Id == questionId);

            if (question != null)
            {
                if (question.ImageData != null)
                {
                    return new MemoryStream(question.ImageData);
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}