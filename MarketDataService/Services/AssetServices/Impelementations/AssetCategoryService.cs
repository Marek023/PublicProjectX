using ProjectX.Enums;
using MarketDataService.Services.AssetServices.Interfaces;

namespace MarketDataService.Services.AssetServices.Impelementations;

public class AssetCategoryService : IAssetCategoryService
{
    public AssetCategoryEnum GetNasdaqAssetCategoryEnum()
    {
        return AssetCategoryEnum.Nasdaq100;
    } 
    public AssetCategoryEnum GetDowJonesCategoryEnum()
    {
        return AssetCategoryEnum.DowJones;
    }
    public AssetCategoryEnum GetSp500AssetCategoryEnum()
    {
        return AssetCategoryEnum.Sp500;
    }
}