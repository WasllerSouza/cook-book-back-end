namespace CookBook.Domain.Entity;

public class EntityBase
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime lastUpdate { get; set; }

    public void prePersist()
    {
        CreatedAt = DateTime.UtcNow;
        lastUpdate = DateTime.UtcNow;
    }
    public void preUpdate()
    {
        lastUpdate = DateTime.UtcNow;
    }
}
