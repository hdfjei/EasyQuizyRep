using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyQuizy.Models
{
    public class FormedQuiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VariantsNumber { get; set; }
        public int QuestionsNumber { get; set; }
        public string GenerationType { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public FormedQuiz()
        {
            Questions = new List<Question>();
        }
    }
}