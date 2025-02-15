using Microsoft.EntityFrameworkCore;
using ProjectX.Data.Entities;

namespace ProjectX.Data.Contexts;


public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<AssetCategories>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Assets>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AssetHistorical>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AssetHistoricalData>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<NewAssets>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ExcludedAssets>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<UserSettingAssetNotificationQueue>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<UserSetting>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<UserRole>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Role>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AssetNotificationQueue>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<YearStatistic>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<MonthStatistic>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TotalYearStatistic>().HasQueryFilter(e => !e.IsDeleted);
        
    }
    
    public DbSet<AssetCategories> AssetCategories { get; set; }
    public DbSet<Assets> Assets { get; set; }
    public DbSet<AssetHistorical> AssetHistorical { get; set; }
    public DbSet<AssetHistoricalData> AssetHistoricalData { get; set; }
    public DbSet<ExcludedAssets> ExcludedAssets { get; set; }
    public DbSet<NewAssets> NewAssets { get; set; }
    public DbSet<UserSettingAssetNotificationQueue> UserSettingAssetNotificationQueue { get; set; }
    public DbSet<UserSetting> UserSetting { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<AssetNotificationQueue> AssetNotificationQueue { get; set; }
    public DbSet<YearStatistic> YearStatistic { get; set; }
    public DbSet<MonthStatistic> MonthStatistic { get; set; }
    public DbSet<TotalYearStatistic> TotalYearStatistic { get; set; }
    
}

