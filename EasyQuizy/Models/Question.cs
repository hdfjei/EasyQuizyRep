using System.Collections.Generic;

namespace EasyQuizy.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public int GeneralQuizId { get; set; }
        public GeneralQuiz GeneralQuiz { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public Question()
        {
            Answers = new List<Answer>();
        }

    }
}