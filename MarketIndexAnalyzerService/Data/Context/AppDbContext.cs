using MarketIndexAnalyzer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketIndexAnalyzer.Data.Context;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public new int SaveChanges()
    {
        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<IndexData>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<IndexType>().HasQueryFilter(e => !e.IsDeleted);
    }
    
    public DbSet<IndexData> IndexData { get; set; }
    public DbSet<IndexType> IndexType { get; set; }
}