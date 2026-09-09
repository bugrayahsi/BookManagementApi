namespace BookManagementAPI.DTOs.Authors;

public class AuthorDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Biography { get; set; }

    public DateTime? BirthDate { get; set; }

    public int BookCount { get; set; }
}