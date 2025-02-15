using MarketIndexAnalyzer.Data.Context;
using MarketIndexAnalyzer.Extensions.Index;
using MarketIndexAnalyzer.Models.Index;
using MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;

namespace MarketIndexAnalyzer.Repositories.IndexRepositories.Implementations;

public class IndexTypeRepository : IIndexTypeRepository
{
    private readonly IAppDbContext _dbContext;

    public IndexTypeRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IndexTypeModel GetIndexTypeById(int indexTypeId)
    {
        var indexType = _dbContext.IndexType.Where(indexType => indexType.Id == indexTypeId)?.FirstOrDefault() 
            ?? throw new NullReferenceException("Error in GetIndexTypeById indexType is null");

        return indexType.ToModel();
    }
}