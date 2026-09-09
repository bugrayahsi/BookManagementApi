namespace BookManagementAPI.DTOs.Authors;

public class AuthorCreateDto
{
    public string Name { get; set; } = string.Empty;

    public string? Biography { get; set; }

    public DateTime? BirthDate { get; set; }
}