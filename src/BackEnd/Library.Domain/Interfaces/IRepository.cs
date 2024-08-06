using Library.Domain.Entities;

namespace Library.Domain.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(Guid id, bool soft = true);
    Task SaveChangesAsync();
}
