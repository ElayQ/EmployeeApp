using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Employee>();
    }
}