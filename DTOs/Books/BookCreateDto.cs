namespace BookManagementAPI.DTOs.Books
{
    public class BookCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public List<int> CategoryIds { get; set; } = new();
    }
}
