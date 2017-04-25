namespace EasyQuizy.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Subject Subject { get; set; }
    }
}