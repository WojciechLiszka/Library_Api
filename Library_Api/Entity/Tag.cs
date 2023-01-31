namespace Library_Api.Entity
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}