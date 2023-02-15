namespace Library_Api.Models
{
    public class CreateBookDto
    {
        public string Tittle { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
    }
}