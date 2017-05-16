using System.Collections.Generic;

namespace EasyQuizy.Models
{
    public class GeneralQuiz
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Question> Questions { get; set; }
        public GeneralQuiz()
        {
            Questions = new List<Question>();
        }
    }
}