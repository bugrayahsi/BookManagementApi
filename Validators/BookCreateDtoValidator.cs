using BookManagementAPI.DTOs.Books;
using FluentValidation;

namespace BookManagementAPI.Validators;

public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
{
    public BookCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.ISBN)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.PublishedYear)
            .InclusiveBetween(1000, DateTime.UtcNow.Year);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.AuthorId)
            .GreaterThan(0);

        RuleFor(x => x.CategoryIds)
            .NotEmpty();
    }
}