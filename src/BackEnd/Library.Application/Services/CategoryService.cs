using AutoMapper;
using Library.Application.Interfaces;
using Library.Communication.Errors;
using Library.Communication.MessagesError;
using Library.Communication.Requests;
using Library.Communication.Responses;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Application.Services;
public class CategoryService(ICategoryRepository repository, IMapper mapper) : ICategoryService
{
    private readonly ICategoryRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<ResponseCategory>>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        var response = _mapper.Map<IEnumerable<ResponseCategory>>(categories);

        return Result.Success(response);
    }

    public async Task<Result<ResponseCategory>> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);

        if (category is null)
            return Result.Failure<ResponseCategory>(CategoryErrors.NotFound(id));

        return _mapper.Map<ResponseCategory>(category);
    }

    public async Task<Result> AddAsync(RequestCategory request)
    {
        var category = _mapper.Map<Category>(request);
;
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid id, RequestCategory request)
    {
        if (id != request.Id)
            return Result.Failure(CategoryErrors.DifferentId(id, request.Id));

        var category = _mapper.Map<Category>(request);

        await _repository.AddAsync(category);
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
