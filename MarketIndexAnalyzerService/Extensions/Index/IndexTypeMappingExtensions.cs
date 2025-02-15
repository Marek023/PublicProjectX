using MarketIndexAnalyzer.Data.Entities;
using MarketIndexAnalyzer.Models.Index;

namespace MarketIndexAnalyzer.Extensions.Index;

public static class IndexTypeMappingExtensions
{
    public static IndexTypeModel ToModel(this IndexType indexType)
    {
        return new IndexTypeModel
        {
            Id = indexType.Id,
            Name = indexType.Name,
            Symbol = indexType.Symbol,
            Description = indexType.Description,
            Currency = indexType.Currency,
            Type = indexType.Type
        };
    }
}