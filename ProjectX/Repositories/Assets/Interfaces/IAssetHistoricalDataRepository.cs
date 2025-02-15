using ProjectX.Models.Assets;

namespace ProjectX.Repositories.Assets.Interfaces;

public interface IAssetHistoricalDataRepository
{
    List<AssetHistoricalDataModel> GetHistoricalDataByAssetId(int assetId);
}