using Library.Domain.Entities;

namespace Library.Domain.Interfaces;
public interface IBookRepository : IRepository<Book> {
    Task<IEnumerable<Book>> GetAllWithCategoryAsync();
    Task<Book?> GetByIsbnAsync(string isbn);
}
