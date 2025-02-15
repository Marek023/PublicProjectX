using ProjectX.Models.Statistic;

namespace ProjectX.Services.Statistics.Interfaces;

public interface IStatisticService
{
    StatisticModel GetStatistic(Guid userId, int year);
}