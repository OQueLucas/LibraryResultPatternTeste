namespace Library.Domain.Entities;
public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
}
