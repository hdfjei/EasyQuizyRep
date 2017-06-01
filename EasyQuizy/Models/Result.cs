using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyQuizy.Models
{
    public class Result
    {
        public int Id { get; set; }
        public double ResultInPercent { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int FormedQuizId { get; set; }
        public FormedQuiz FormedQuiz { get; set; }
    }
}