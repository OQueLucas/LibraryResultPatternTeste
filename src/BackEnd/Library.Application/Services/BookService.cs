using AutoMapper;
using Library.Application.Interfaces;
using Library.Application.Validation;
using Library.Communication.MessagesError;
using Library.Communication.Requests;
using Library.Communication.Responses;
using Library.Communication.Results;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.Services;
public class BookService(IBookRepository repository, IMapper mapper) : IBookService
{
    private readonly IBookRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<ResponseBook>>> GetAllAsync()
    {
        var books = await _repository.GetAllWithCategoryAsync();
        var response = _mapper.Map<IEnumerable<ResponseBook>>(books);

        return Result.Success(response);
    }

    public async Task<Result<ResponseBook>> GetByIdAsync(Guid id)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book is null)
            return Result.Failure<ResponseBook>(BookErrors.NotFound(id));

        var response = _mapper.Map<ResponseBook>(book);

        return Result.Success(response);
    }

    public async Task<Result<ResponseBook>> GetByIsbnAsync(string isbn)
    {
        var book = await _repository.GetByIsbnAsync(isbn);

        if (book is null)
            return Result.Failure<ResponseBook>(BookErrors.NotFoundByISBN(isbn));

        var response = _mapper.Map<ResponseBook>(book);

        return Result.Success(response);
    }

    public async Task<Result> AddAsync(RequestBook request)
    {
        var validator = new CreateBookValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            return Result.Failure(Error.Validation("Books.Validation", errorMessages));
        }

        var queryBook = GetByIsbnAsync(request.ISBN);

        if (queryBook.Result is not null)
            return Result.Failure(BookErrors.IsbnNotUnique());

        var book = _mapper.Map<Book>(request);

        await _repository.AddAsync(book);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid id, RequestBook request)
    {
        if (id != request.Id) 
            return Result.Failure(BookErrors.DifferentId(id, request.Id));

        var book = _mapper.Map<Book>(request);

        _repository.Update(book);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }
}
