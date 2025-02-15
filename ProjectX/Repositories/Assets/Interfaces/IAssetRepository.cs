using ProjectX.Models.Assets;

namespace ProjectX.Repositories.Assets.Interfaces;

public interface IAssetRepository
{
    List<AssetWithHistoricalDataModel> GetAssetWithHistoricalData(int assetCategoryId);
}