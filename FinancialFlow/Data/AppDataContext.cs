using System.Reflection;
using FinancialCore.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialFlow.Data;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}