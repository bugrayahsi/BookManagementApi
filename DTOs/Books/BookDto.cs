namespace BookManagementAPI.DTOs.Books
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; } = string.Empty;

        public List<string> Categories { get; set; } = new();
    }
}
