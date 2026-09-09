using BookManagementAPI.Data;
using BookManagementAPI.Models;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories 
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Book> Books, int TotalCount)> GetPagedAsync(
    int page,
    int pageSize,
    string? search,
    int? authorId,
    int? categoryId,
    string? sortBy,
    string? sortOrder)
        {
            var query = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Categories)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b =>
                    b.Title.Contains(search) ||
                    (b.Description != null &&
                     b.Description.Contains(search)));
            }

            if (authorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == authorId.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(b =>
                    b.Categories.Any(c => c.Id == categoryId.Value));
            }

            var totalCount = await query.CountAsync();

            sortBy = sortBy?.ToLower();
            sortOrder = sortOrder?.ToLower();

            query = sortBy switch
            {
                "title" => sortOrder == "desc"
                    ? query.OrderByDescending(b => b.Title)
                    : query.OrderBy(b => b.Title),

                "price" => sortOrder == "desc"
                    ? query.OrderByDescending(b => b.Price)
                    : query.OrderBy(b => b.Price),

                "publishedyear" => sortOrder == "desc"
                    ? query.OrderByDescending(b => b.PublishedYear)
                    : query.OrderBy(b => b.PublishedYear),

                "createdat" => sortOrder == "desc"
                    ? query.OrderByDescending(b => b.CreatedAt)
                    : query.OrderBy(b => b.CreatedAt),

                _ => query.OrderBy(b => b.Id)
            };

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (books, totalCount);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
