using System.Collections.Generic;

namespace EasyQuizy.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GeneralQuiz> GeneralQuizes { get; set; }

        public Subject()
        {
            GeneralQuizes = new List<GeneralQuiz>();
        }
    }
}