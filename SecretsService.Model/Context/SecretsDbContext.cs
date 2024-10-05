using Microsoft.EntityFrameworkCore;

namespace SecretsService.DataAccess.Context;

public class SecretsDbContext : DbContext
{
    public SecretsDbContext(DbContextOptions<SecretsDbContext> options) : base(options)
    {
    }

    public DbSet<Secret> Secrets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Secret>()
            .HasIndex(s => s.Name)
            .IsUnique();
    }
}