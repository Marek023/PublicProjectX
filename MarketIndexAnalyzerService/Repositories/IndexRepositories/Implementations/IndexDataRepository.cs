using MarketIndexAnalyzer.Data.Context;
using MarketIndexAnalyzer.Data.Entities;
using MarketIndexAnalyzer.Extensions.Index;
using MarketIndexAnalyzer.Models.Index;
using MarketIndexAnalyzer.Models.NasdaqIShares;
using MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;

namespace MarketIndexAnalyzer.Repositories.IndexRepositories.Implementations;

public class IndexDataRepository : IIndexDataRepository 
{
    private readonly IAppDbContext _dbContext;

    public IndexDataRepository(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveData(List<RecordModelJson> records, IndexTypeModel indexType)
    {
        var timeFromIndexData = new HashSet<DateTime>(GetAllDataByIndexTypeId(indexType.Id).Select(data => data.Time));
        
        foreach (var record in records)
        {
            if(timeFromIndexData.Contains(record.Time)) continue;
            
            var indexData = new IndexData()
            {
                Open = record.Open,
                Close = record.Close,
                High = record.High,
                Low = record.Low,
                Time = record.Time.ToUniversalTime(),
                IndexTypeId = indexType.Id
            };
            
            _dbContext.IndexData.Add(indexData);
        }
        
        _dbContext.SaveChanges();
    }

    public List<IndexDataModel> GetAllDataByIndexTypeId(int indexTypeId)
    {
        return _dbContext.IndexData.Where(indexData => indexData.IndexTypeId == indexTypeId)
            .ToList()
            .ToModelList();
    }
}