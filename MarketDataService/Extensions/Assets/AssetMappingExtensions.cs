using MarketDataService.Models.Json;

namespace MarketDataService.Extensions.Assets;

public static class AssetMappingExtensions
{
    public static ProjectX.Data.Entities.Assets ToEntity(this AssetJsonModel assetJsonModel)
    {
        if (assetJsonModel == null)
        {
            throw new ArgumentNullException(nameof(assetJsonModel));
        }

        return new ProjectX.Data.Entities.Assets
        {
            Symbol = assetJsonModel.symbol,
            Name = assetJsonModel.name,
            Sector = assetJsonModel.sector,
            SubSector = assetJsonModel.subSector,
            DateFirstAdded = assetJsonModel.dateFirstAdded,
            Founded = assetJsonModel.founded,
            DateSave = DateTime.UtcNow,
            HeadQuarter = assetJsonModel.headQuarter,
            Cik = assetJsonModel.cik,
        };
    }

   
}