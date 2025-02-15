using ProjectX.Models.Assets;

namespace ProjectX.Services.DowJones.Interfaces;

public interface IDowJonesService
{
    List<AssetMarketDataModel> GetDataForTable();
}