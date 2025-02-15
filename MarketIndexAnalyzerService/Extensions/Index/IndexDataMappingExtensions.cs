using MarketIndexAnalyzer.Data.Entities;
using MarketIndexAnalyzer.Models.Index;
using MarketIndexAnalyzer.Models.NasdaqIShares;

namespace MarketIndexAnalyzer.Extensions.Index;

public static class IndexDataMappingExtensions
{
    // public static IndexData ToEntity(this Record record)
    // {
    //     return new IndexData
    //     {
    //         Open = record.Open,
    //         Close = record.Close,
    //         High = record.High,
    //         Low = record.Low,
    //         Time = record.Time,
    //     };
    // }

    public static List<IndexDataModel> ToModelList(this List<IndexData> indexDatas)
    {
        return indexDatas.Select(indexData => new IndexDataModel
        {
            Id = indexData.Id,
            Open = indexData.Open,
            Close = indexData.Close,
            High = indexData.High,
            Low = indexData.Low,
            Time = indexData.Time.ToLocalTime(),
            IndexTypeId = indexData.IndexTypeId,
        }).ToList();
    }
}