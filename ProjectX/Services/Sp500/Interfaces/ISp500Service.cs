using ProjectX.Models.Assets;

namespace ProjectX.Services.Sp500.Interfaces;

public interface ISp500Service
{
    List<AssetMarketDataModel> GetDataForTable();
}