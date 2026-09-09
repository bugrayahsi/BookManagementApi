using BookManagementAPI.DTOs.Authors;

namespace BookManagementAPI.Services.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorDto>> GetAllAsync();

    Task<AuthorDto?> GetByIdAsync(int id);

    Task AddAsync(AuthorCreateDto dto);

    Task<bool> UpdateAsync(int id, AuthorUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}