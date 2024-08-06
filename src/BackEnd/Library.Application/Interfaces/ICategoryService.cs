using Library.Communication.Results;
using Library.Communication.Requests;
using Library.Communication.Responses;

namespace Library.Application.Interfaces;
public interface ICategoryService
{
    Task<Result<IEnumerable<ResponseCategory>>> GetAllAsync();
    Task<Result<ResponseCategory>> GetByIdAsync(Guid id);
    Task<Result> AddAsync(RequestCategory request);
    Task<Result> UpdateAsync(Guid id, RequestCategory request);
    Task<Result> DeleteAsync(Guid id);
}
