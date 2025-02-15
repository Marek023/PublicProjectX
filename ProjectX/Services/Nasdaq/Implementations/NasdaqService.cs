using ProjectX.Enums;
using ProjectX.Helpers.Assets.DataForWeb.Interfaces;
using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;
using ProjectX.Services.Nasdaq.Interfaces;

namespace ProjectX.Services.Nasdaq.Implementations;

public class NasdaqService : INasdaqService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IBaseDataForTableService _baseDataForTableService;

    public NasdaqService(IAssetRepository assetRepository, IBaseDataForTableService baseDataForTableService)
    {
        _assetRepository = assetRepository;
        _baseDataForTableService = baseDataForTableService;
    }

    public List<AssetMarketDataModel> GetDataForTable()
    {
        var assetsWithHistoricalData = _assetRepository.GetAssetWithHistoricalData((int)AssetCategoryEnum.Nasdaq100);

        return _baseDataForTableService.GetDataForTable(assetsWithHistoricalData);
    }
}