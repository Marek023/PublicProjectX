using ProjectX.Enums;
using ProjectX.Models.Assets;

namespace ProjectX.Helpers.Assets.DataForWeb.Interfaces;

public interface IBaseDataForTableService
{
    public List<AssetMarketDataModel> GetDataForTable(List<AssetWithHistoricalDataModel> assetsWithHistoricalData);
}