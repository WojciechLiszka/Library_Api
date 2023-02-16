using Library_Api.Entity;

namespace Library_Api.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public List<string> Tags{ get; set; }
        public bool IsAvailable { get; set; }
    }
} 