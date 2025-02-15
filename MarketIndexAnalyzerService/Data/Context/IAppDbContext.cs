using MarketIndexAnalyzer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketIndexAnalyzer.Data.Context;

public interface IAppDbContext
{
    public DbSet<IndexData> IndexData { get; set; }
    public DbSet<IndexType> IndexType { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();

}