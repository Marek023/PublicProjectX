using ProjectX.Models.Statistic;
using ProjectX.Repositories.Statistics.Interfaces;
using ProjectX.Services.Statistics.Interfaces;

namespace ProjectX.Services.Statistics.Implementations;

public class StatisticService : IStatisticService
{
    private readonly IStatisticRepository _statisticRepository;

    public StatisticService(IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }

    public StatisticModel GetStatistic(Guid userId, int year)
    {
        return _statisticRepository.GetStatistic(userId, year);
    }
}