using BookManagementAPI.DTOs.Categories;

namespace BookManagementAPI.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();

    Task<CategoryDto?> GetByIdAsync(int id);

    Task AddAsync(CategoryCreateDto dto);

    Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}