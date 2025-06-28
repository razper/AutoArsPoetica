using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

public class ArsPoeticaDbContext : DbContext
{
    public ArsPoeticaDbContext(DbContextOptions<ArsPoeticaDbContext> options) : base(options)
    {
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    public DbSet<Poem> Poems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Poem>().ToCollection("poems");
    }
}
