namespace Library_Api.Entity
{
    public class Book
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Rent> Rents { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}