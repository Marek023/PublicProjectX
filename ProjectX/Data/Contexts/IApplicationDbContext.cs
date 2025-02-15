using Microsoft.EntityFrameworkCore;
using ProjectX.Data.Entities;

namespace ProjectX.Data.Contexts;

public interface IApplicationDbContext
{
    DbSet<AssetCategories> AssetCategories { get; set; }
    DbSet<Assets> Assets { get; set; }
    DbSet<AssetHistorical> AssetHistorical { get; set; }
    DbSet<AssetHistoricalData> AssetHistoricalData { get; set; }
    DbSet<ExcludedAssets> ExcludedAssets { get; set; }
    DbSet<NewAssets> NewAssets { get; set; }
    public DbSet<UserSettingAssetNotificationQueue> UserSettingAssetNotificationQueue { get; set; }
    public DbSet<UserSetting> UserSetting { get; set; }
    public DbSet<UserRole> UserRole { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<AssetNotificationQueue> AssetNotificationQueue { get; set; }
    public DbSet<YearStatistic> YearStatistic { get; set; }
    public DbSet<MonthStatistic> MonthStatistic { get; set; }
    public DbSet<TotalYearStatistic> TotalYearStatistic { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}