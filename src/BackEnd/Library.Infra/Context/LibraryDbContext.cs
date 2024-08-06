using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infra.Context;
public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
    }
}