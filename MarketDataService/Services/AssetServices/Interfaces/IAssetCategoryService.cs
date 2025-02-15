using ProjectX.Enums;

namespace MarketDataService.Services.AssetServices.Interfaces;

public interface IAssetCategoryService
{
    public AssetCategoryEnum GetNasdaqAssetCategoryEnum();
    public AssetCategoryEnum GetDowJonesCategoryEnum();
    public AssetCategoryEnum GetSp500AssetCategoryEnum();
}