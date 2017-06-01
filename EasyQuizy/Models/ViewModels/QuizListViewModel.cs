using System.Collections.Generic;
using System.Web.Mvc;

namespace EasyQuizy.Models.ViewModels
{
    public class QuizListViewModel
    {
        public IEnumerable<GeneralQuiz> GeneralQuizes { get; set; }
        public SelectList Subjects { get; set; }
        public SelectList Categories { get; set; }
        public string FormedQuizName { get; set; }
        public int[] Quizes { get; set; }
        public bool[] IsChoosen { get; set; }
        public int VariantsNumber { get; set; }
        public int QuestionsNumber { get; set; }
    }
}