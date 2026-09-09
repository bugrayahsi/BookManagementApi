using AutoMapper;
using BookManagementAPI.DTOs.Categories;
using BookManagementAPI.Models;
using BookManagementAPI.Repositories.Interfaces;
using BookManagementAPI.Services.Interfaces;

namespace BookManagementAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();

        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task AddAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);

        _repository.Add(category);

        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(
        int id,
        CategoryUpdateDto dto)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            return false;
        }

        _mapper.Map(dto, category);

        _repository.Update(category);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category == null)
        {
            return false;
        }

        _repository.Delete(category);

        await _repository.SaveChangesAsync();

        return true;
    }
}