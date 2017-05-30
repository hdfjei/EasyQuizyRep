using System.Data.Entity;

namespace EasyQuizy.Models
{
    public class QuizContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GeneralQuiz> GeneralQuizes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<FormedQuiz> FormedQuizes { get; set; }
    }
}