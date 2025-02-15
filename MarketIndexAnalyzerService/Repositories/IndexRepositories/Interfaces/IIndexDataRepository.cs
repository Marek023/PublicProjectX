using MarketIndexAnalyzer.Data.Entities;
using MarketIndexAnalyzer.Models.Index;
using MarketIndexAnalyzer.Models.NasdaqIShares;

namespace MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;

public interface IIndexDataRepository
{
    void SaveData(List<RecordModelJson> records, IndexTypeModel indexType);
    List<IndexDataModel> GetAllDataByIndexTypeId(int indexTypeId);
}