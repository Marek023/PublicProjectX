using ProjectX.Data.Contexts;
using ProjectX.Models.Statistic;
using ProjectX.Repositories.Statistics.Interfaces;

namespace ProjectX.Repositories.Statistics.Implementations;

public class StatisticRepository : IStatisticRepository
{
    private readonly IApplicationDbContext _dbContext;

    public StatisticRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public StatisticModel GetStatistic(Guid userId, int year)
    {
        var yearStatistics =
            _dbContext.YearStatistic.Where(statistic => statistic.Year == year && statistic.UserId == userId)
                .OrderBy(statistic => statistic.MonthNumber)
                .ToList();

        if (!yearStatistics.Any())
            return new StatisticModel();

        var statisticModel = new StatisticModel();
        statisticModel.YearStatisticModels = new List<YearStatisticModel>();

        foreach (var yearStatistic in yearStatistics)
        {
            var yearStatisticModel = new YearStatisticModel
            {
                Id = yearStatistic.Id,
                Year = yearStatistic.Year,
                MonthNumber = yearStatistic.MonthNumber,
                MonthName = yearStatistic.MonthName,
                TotalDeposit = yearStatistic.TotalDeposit,
                Profit = yearStatistic.Profit,
                Dividend = yearStatistic.Dividend,
                PofitInPercent = GetStringProfitInPercent(yearStatistic.ProfitInPercent),
                TotalYearStatisticModel = new TotalYearStatisticModel
                {
                    Id = yearStatistic.TotalYearStatistic.Id,
                    Year = yearStatistic.TotalYearStatistic.Year,
                    MonthNumber = yearStatistic.TotalYearStatistic.Month,
                    TotalDivident = yearStatistic.TotalYearStatistic.TotalDivident,
                    TotalDeposit = yearStatistic.TotalYearStatistic.TotalDeposit,
                    TotalProfitForYear = yearStatistic.TotalYearStatistic.TotalProfitForYear,
                    ProfitInPercentForYear = GetStringProfitInPercent(yearStatistic.TotalYearStatistic.ProfitInPercentForYear)
                }
            };

            statisticModel.YearStatisticModels.Add(yearStatisticModel);
        }

        return statisticModel;
    }

    private string GetStringProfitInPercent(decimal profitInPercent)
    {
        return $"{profitInPercent * 100:F2} %";
    }
}