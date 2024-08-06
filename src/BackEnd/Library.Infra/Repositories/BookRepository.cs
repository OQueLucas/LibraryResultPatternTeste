using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Infra.Repositories;
public class BookRepository : IBookRepository {

    private readonly LibraryDbContext _context;
    private readonly DbSet<Book> _dbSet;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Book>();
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(book => book.Category)
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<IEnumerable<Book>> GetAllWithCategoryAsync()
    {
        return await _context.Books
            .Include(book => book.Category)
            .ToListAsync();
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await _context.Books
            .Include(book => book.Category)
            .FirstOrDefaultAsync(book => book.ISBN == isbn);
    }

    public async Task AddAsync(Book entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(Book entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteAsync(Guid id, bool soft = true)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
            return;

        if (soft)
        {
            entity.IsDeleted = true;
            _context.Update(entity);
            return;
        }

        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
