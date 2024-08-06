using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Infra.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly LibraryDbContext _context;
    private readonly DbSet<Category> _dbSet;

    public CategoryRepository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Category>();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task AddAsync(Category entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(Category entity)
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
