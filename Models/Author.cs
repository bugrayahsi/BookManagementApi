namespace BookManagementAPI.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Biography { get; set; }

        public DateTime? BirthDate { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
