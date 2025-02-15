using ProjectX.Models.Statistic;

namespace ProjectX.Repositories.Statistics.Interfaces;

public interface IStatisticRepository
{
    StatisticModel GetStatistic(Guid userId, int year);
}