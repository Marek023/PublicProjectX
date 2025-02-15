using MarketIndexAnalyzer.Models.Index;

namespace MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;

public interface IIndexTypeRepository
{
    IndexTypeModel GetIndexTypeById(int indexTypeId);
}