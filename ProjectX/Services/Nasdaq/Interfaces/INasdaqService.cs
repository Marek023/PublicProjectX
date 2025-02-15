using ProjectX.Models.Assets;

namespace ProjectX.Services.Nasdaq.Interfaces;

public interface INasdaqService
{
    List<AssetMarketDataModel> GetDataForTable();
}