using MarketDataService.Models.Asset;
using ProjectX.Data.Entities;

namespace MarketDataService.Extensions.Assets;

public static class NewAssetMappingExtensions
{
    public static AssetModel ToModel(this NewAssets newAsset)
    {
        if (newAsset == null)
        {
            throw new ArgumentNullException(nameof(newAsset));
        }

        return new AssetModel
        {
            Id = newAsset.Id,
            Symbol = newAsset.Symbol,
            Name = newAsset.Name,
            Sector = newAsset.Sector,
            SubSector = newAsset.SubSector,
            HeadQuarter = newAsset.HeadQuarter,
            DateFirstAdded = newAsset.DateFirstAdded,
            Cik = newAsset.Cik,
            Founded = newAsset.Founded,
            DateSave = DateTime.UtcNow,
            AssetCategoryId = newAsset.AssetCategoryId
        };
    }
    
    public static List<AssetModel> ToModelList(this List<NewAssets> newAssets)
    {
        return newAssets.Select(newAsset => new AssetModel
        {
            Id = newAsset.Id,
            Symbol = newAsset.Symbol,
            Name = newAsset.Name,
            Sector = newAsset.Sector,
            SubSector = newAsset.SubSector,
            HeadQuarter = newAsset.HeadQuarter,
            DateFirstAdded = newAsset.DateFirstAdded,
            Cik = newAsset.Cik,
            Founded = newAsset.Founded,
            DateSave = DateTime.UtcNow,
            AssetCategoryId = newAsset.AssetCategoryId,
            AssetCategoryName = newAsset.AssetCategory?.Name
        }).ToList();
    }
}