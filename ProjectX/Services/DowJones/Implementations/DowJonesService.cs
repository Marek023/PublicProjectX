using ProjectX.Enums;
using ProjectX.Helpers.Assets.DataForWeb.Interfaces;
using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;
using ProjectX.Services.DowJones.Interfaces;

namespace ProjectX.Services.DowJones.Implementations;

public class DowJonesService : IDowJonesService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IBaseDataForTableService _baseDataForTableService;
    
    public DowJonesService(IAssetRepository assetRepository, IBaseDataForTableService baseDataForTableService)
    {
        _assetRepository = assetRepository;
        _baseDataForTableService = baseDataForTableService;
    }

    public List<AssetMarketDataModel> GetDataForTable()
    {
        var assetsWithHistoricalData =_assetRepository.GetAssetWithHistoricalData((int)AssetCategoryEnum.DowJones);
        
        return _baseDataForTableService.GetDataForTable(assetsWithHistoricalData);
    }
}