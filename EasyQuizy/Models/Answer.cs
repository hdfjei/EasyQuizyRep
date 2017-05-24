namespace EasyQuizy.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public bool IsTrue { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}