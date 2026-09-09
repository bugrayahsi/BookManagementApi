using AutoMapper;
using BookManagementAPI.DTOs.Authors;
using BookManagementAPI.Models;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper;

    public AuthorService(
        IAuthorRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AuthorDto>> GetAllAsync()
    {
        var authors = await _repository.GetAllAsync();

        return _mapper.Map<List<AuthorDto>>(authors);
    }

    public async Task<AuthorDto?> GetByIdAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);

        if (author == null)
        {
            return null;
        }

        return _mapper.Map<AuthorDto>(author);
    }

    public async Task AddAsync(AuthorCreateDto dto)
    {
        var author = _mapper.Map<Author>(dto);

        _repository.Add(author);

        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, AuthorUpdateDto dto)
    {
        var author = await _repository.GetByIdAsync(id);

        if (author == null)
        {
            return false;
        }

        _mapper.Map(dto, author);

        _repository.Update(author);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var author = await _repository.GetByIdAsync(id);

        if (author == null)
        {
            return false;
        }

        _repository.Delete(author);

        await _repository.SaveChangesAsync();

        return true;
    }
}