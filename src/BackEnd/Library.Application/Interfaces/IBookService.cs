using Library.Communication.Errors;
using Library.Communication.Requests;
using Library.Communication.Responses;

namespace Library.Application.Interfaces;
public interface IBookService
{
    Task<Result<IEnumerable<ResponseBook>>> GetAllAsync();
    Task<Result<ResponseBook>> GetByIdAsync(Guid id);
    Task<Result<ResponseBook>> GetByIsbnAsync(string isbn);
    Task<Result> AddAsync(RequestBook request);
    Task<Result> UpdateAsync(Guid id, RequestBook request);
    Task<Result> DeleteAsync(Guid id);
}
