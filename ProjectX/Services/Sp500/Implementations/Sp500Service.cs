using ProjectX.Enums;
using ProjectX.Helpers.Assets.DataForWeb.Interfaces;
using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;
using ProjectX.Services.Sp500.Interfaces;

namespace ProjectX.Services.Sp500.Implementations;

public class Sp500Service : ISp500Service
{
    private readonly IAssetRepository _assetRepository;
    private readonly IBaseDataForTableService _baseDataForTableService;

    public Sp500Service(IAssetRepository assetRepository, IBaseDataForTableService baseDataForTableService)
    {
        _assetRepository = assetRepository;
        _baseDataForTableService = baseDataForTableService;
    }

    public List<AssetMarketDataModel> GetDataForTable()
    {
        var assetsWithHistoricalData =_assetRepository.GetAssetWithHistoricalData((int)AssetCategoryEnum.Sp500);
        
        return _baseDataForTableService.GetDataForTable(assetsWithHistoricalData);
    }
    
   
}