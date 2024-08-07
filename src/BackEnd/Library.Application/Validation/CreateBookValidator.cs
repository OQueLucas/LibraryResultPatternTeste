using FluentValidation;
using Library.Communication.MessagesError;
using Library.Communication.Requests;

namespace Library.Application.Validation;

internal class CreateBookValidator : AbstractValidator<RequestBook>
{
    public CreateBookValidator()
    {
        RuleFor(request => request.Title).NotEmpty().WithMessage(ErrorMessages.AUTHOR_EMPTY());
        RuleFor(request => request.ISBN).NotEmpty().WithMessage(ErrorMessages.ISBN_EMPTY());
        RuleFor(request => request.Author).NotEmpty().WithMessage(ErrorMessages.AUTHOR_EMPTY());
    }
}
