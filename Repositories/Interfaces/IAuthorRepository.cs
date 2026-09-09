using BookManagementAPI.Models;

namespace BookManagementAPI.Repositories.Interfaces;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAsync();

    Task<Author?> GetByIdAsync(int id);

    void Add(Author author);

    void Update(Author author);

    void Delete(Author author);

    Task SaveChangesAsync();

    Task<bool> ExistsAsync(int id);
}