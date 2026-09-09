using BookManagementAPI.DTOs.Books;
using BookManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }
    [HttpGet]
    [HttpGet]
    public async Task<IActionResult> GetAll(
    int page = 1,
    int pageSize = 10,
    string? search = null,
    int? authorId = null,
    int? categoryId = null,
    string? sortBy = null,
    string? sortOrder = null)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        if (pageSize > 100)
        {
            pageSize = 100;
        }

        var books = await _service.GetPagedAsync(
            page,
            pageSize,
            search,
            authorId,
            categoryId,
            sortBy,
            sortOrder);

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookCreateDto dto)
    {
        await _service.AddAsync(dto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookUpdateDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}