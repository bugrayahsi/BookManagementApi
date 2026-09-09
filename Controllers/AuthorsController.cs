using BookManagementAPI.DTOs.Authors;
using BookManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorsController(IAuthorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _service.GetAllAsync();

        return Ok(authors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorCreateDto dto)
    {
        await _service.AddAsync(dto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        AuthorUpdateDto dto)
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