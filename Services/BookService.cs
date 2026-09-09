using AutoMapper;
using BookManagementAPI.DTOs.Books;
using BookManagementAPI.Helpers;
using BookManagementAPI.Models;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;
using FluentValidation;
using BookManagementAPI.Exceptions;

namespace BookManagementAPI.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    private readonly IValidator<BookCreateDto> _createValidator;
    private readonly IValidator<BookUpdateDto> _updateValidator;

    private readonly ICategoryRepository _categoryRepository;
    private readonly IAuthorRepository _authorRepository;
    public BookService(
     IBookRepository repository,
     IMapper mapper,
     IValidator<BookCreateDto> createValidator,
     IValidator<BookUpdateDto> updateValidator,
     ICategoryRepository categoryRepository,
     IAuthorRepository authorRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _categoryRepository = categoryRepository;
        _authorRepository = authorRepository;
    }

    public async Task<PagedResult<BookDto>> GetPagedAsync(
     int page,
     int pageSize,
     string? search,
     int? authorId,
     int? categoryId,
     string? sortBy,
     string? sortOrder)
    {
        var result = await _repository.GetPagedAsync(
            page,
            pageSize,
            search,
            authorId,
            categoryId,
            sortBy,
            sortOrder);

        var books = _mapper.Map<List<BookDto>>(result.Books);

        return new PagedResult<BookDto>
        {
            Items = books,
            Page = page,
            PageSize = pageSize,
            TotalCount = result.TotalCount,
            TotalPages = (int)Math.Ceiling(
                result.TotalCount / (double)pageSize)
        };
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        return _mapper.Map<BookDto>(book);
    }

    public async Task AddAsync(BookCreateDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        var authorExists = await _authorRepository.ExistsAsync(dto.AuthorId);

        if (!authorExists)
        {
            throw new NotFoundException("Author not found.");
        }

        var categoryIds = dto.CategoryIds.Distinct().ToList();

        var categories = await _categoryRepository
            .GetByIdsAsync(categoryIds);

        if (categories.Count != categoryIds.Count)
        {
            throw new NotFoundException(
                "One or more categories not found.");
        }

        var book = _mapper.Map<Book>(dto);

        book.Categories = categories;
        book.CreatedAt = DateTime.UtcNow;

        _repository.Add(book);

        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, BookUpdateDto dto)
    {
        await _updateValidator.ValidateAndThrowAsync(dto);

        var book = await _repository.GetByIdAsync(id);

        if (book == null)
        {
            return false;
        }

        var authorExists = await _authorRepository.ExistsAsync(dto.AuthorId);

        if (!authorExists)
        {
            throw new NotFoundException("Author not found.");
        }

        var categoryIds = dto.CategoryIds.Distinct().ToList();

        var categories = await _categoryRepository
            .GetByIdsAsync(categoryIds);

        if (categories.Count != categoryIds.Count)
        {
            throw new NotFoundException(
                "One or more categories not found.");
        }

        _mapper.Map(dto, book);

        book.Categories.Clear();

        foreach (var category in categories)
        {
            book.Categories.Add(category);
        }

        book.UpdatedAt = DateTime.UtcNow;

        _repository.Update(book);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
        {
            return false;
        }

        _repository.Delete(book);

        await _repository.SaveChangesAsync();

        return true;
    }
}