using AutoMapper;
using BookManagementAPI.DTOs.Authors;
using BookManagementAPI.DTOs.Books;
using BookManagementAPI.DTOs.Categories;
using BookManagementAPI.Models;

namespace BookManagementAPI.Helpers;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(
                dest => dest.Categories,
                opt => opt.MapFrom(src => src.Categories.Select(c => c.Name)));

        CreateMap<BookCreateDto, Book>()
            .ForMember(
                dest => dest.Categories,
                opt => opt.Ignore());

        CreateMap<BookUpdateDto, Book>()
            .ForMember(
                dest => dest.Categories,
                opt => opt.Ignore());

        CreateMap<Author, AuthorDto>()
            .ForMember(
                dest => dest.BookCount,
                opt => opt.MapFrom(src => src.Books.Count));

        CreateMap<AuthorCreateDto, Author>();

        CreateMap<AuthorUpdateDto, Author>();

        CreateMap<Category, CategoryDto>()
    .ForMember(
        dest => dest.BookCount,
        opt => opt.MapFrom(src => src.Books.Count));

        CreateMap<CategoryCreateDto, Category>();

        CreateMap<CategoryUpdateDto, Category>();
    }
}