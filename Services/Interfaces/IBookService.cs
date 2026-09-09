using BookManagementAPI.DTOs.Books;
using BookManagementAPI.Helpers;
using BookManagementAPI.Models;

namespace BookManagementAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<PagedResult<BookDto>> GetPagedAsync(
    int page,
    int pageSize,
    string? search,
    int? authorId,
    int? categoryId);

        Task<BookDto?> GetByIdAsync(int id);

        Task AddAsync(BookCreateDto dto);

        Task<bool> UpdateAsync(int id, BookUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
