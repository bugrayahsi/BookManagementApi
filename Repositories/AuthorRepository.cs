using BookManagementAPI.Data;
using BookManagementAPI.Models;
using BookManagementAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await _context.Authors
            .Include(a => a.Books)
            .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public void Add(Author author)
    {
        _context.Authors.Add(author);
    }

    public void Update(Author author)
    {
        _context.Authors.Update(author);
    }

    public void Delete(Author author)
    {
        _context.Authors.Remove(author);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Authors
            .AnyAsync(a => a.Id == id);
    }
}