namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string ISBN { get; set; } = string.Empty;

        public int PublishedYear { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Author Author { get; set; } = null!;

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
