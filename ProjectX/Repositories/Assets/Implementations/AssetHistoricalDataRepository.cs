using ProjectX.Data.Contexts;
using ProjectX.Data.Entities;
using ProjectX.Extensions.Assets;
using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;

namespace ProjectX.Repositories.Assets.Implementations;

public class AssetHistoricalDataRepository : IAssetHistoricalDataRepository
{
    private readonly IApplicationDbContext _dbContext;

    public AssetHistoricalDataRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<AssetHistoricalDataModel> GetHistoricalDataByAssetId(int assetId)
    {
        return _dbContext.AssetHistoricalData.Where(historicalData => historicalData.AssetsId == assetId)
            .ToList()
            .ToModelList(); 
    }
}