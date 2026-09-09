using BookManagementAPI.Models;

namespace BookManagementAPI.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<(List<Book> Books, int TotalCount)> GetPagedAsync(
    int page,
    int pageSize,
    string? search,
    int? authorId,
    int? categoryId,
    string? sortBy,
    string? sortOrder);
        Task<Book?> GetByIdAsync(int id);

        void Add(Book book);

        void Update(Book book);

        void Delete(Book book);

        Task SaveChangesAsync();
    }
}
