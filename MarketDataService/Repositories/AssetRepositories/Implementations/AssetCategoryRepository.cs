using MarketDataService.Repositories.AssetRepositories.Interfaces;
using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;

namespace MarketDataService.Repositories.AssetRepositories.Implementations;

public class AssetCategoryRepository : IAssetCategoryRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetCategoryRepository(IApplicationDbContext context)
    {
        _dbContext = context;
    }

    public AssetCategories GetAssetCategory(int assetCategoryId)
    {
        return _dbContext.AssetCategories.FirstOrDefault(assetCategory => assetCategory.Id == assetCategoryId)
               ?? throw new ArgumentException($"AssetCategory with id {assetCategoryId} not found");
    }
}