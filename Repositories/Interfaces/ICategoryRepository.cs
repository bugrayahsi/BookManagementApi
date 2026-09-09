using BookManagementAPI.Models;

namespace BookManagementAPI.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task<List<Category>> GetByIdsAsync(List<int> ids);
}