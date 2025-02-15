using MarketDataService.Models.Asset;
using ProjectX.Data.Entities;

namespace MarketDataService.Extensions.Assets;

public static class ExcludedAssetMappingExtensions
{
    public static List<AssetModel> ToModelList(this List<ExcludedAssets> excludedAssets)
    {
        return excludedAssets.Select(excludedAsset => new AssetModel
        {
            Id = excludedAsset.Id,
            Symbol = excludedAsset.Symbol,
            Name = excludedAsset.Name,
            Sector = excludedAsset.Sector,
            SubSector = excludedAsset.SubSector,
            HeadQuarter = excludedAsset.HeadQuarter,
            DateFirstAdded = excludedAsset.DateFirstAdded,
            Cik = excludedAsset.Cik,
            Founded = excludedAsset.Founded,
            DateSave = DateTime.UtcNow,
            AssetCategoryId = excludedAsset.AssetCategoryId,
            AssetCategoryName = excludedAsset.AssetCategory?.Name
        }).ToList();
    }

}