using Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.Context;

public class AppDbContext : DbContext
{
    public DbSet<PersonDb> Persons { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}