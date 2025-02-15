using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;
using ProjectX.Services.Calculators.Interfaces;

namespace ProjectX.Services.Calculators.Implementations;

public class CompoundInterestFromHistoricalDataService : ICompoundInterestFromHistoricalDataService
{
    private readonly IServiceScopeFactory _serviceScopeFactoryscopeFactory;

    public CompoundInterestFromHistoricalDataService(IServiceScopeFactory scopeFactory)
    {
        _serviceScopeFactoryscopeFactory = scopeFactory;
    }

    public void GetResult()
    {
        using (var scope = _serviceScopeFactoryscopeFactory.CreateScope())
        {
            var historicalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                           ?? throw new NullReferenceException("AssetHistoricalDataRepository is null");

            var historicalDataModels = historicalDataRepository.GetHistoricalDataByAssetId(1);
            var endDate = DateTime.Now;
            var startDate = endDate.AddYears(-10);

            Calculation(historicalDataModels, startDate, endDate, 0, 833);
        }
    }

    private void Calculation(
        List<AssetHistoricalDataModel> historicalData,
        DateTime startDate,
        DateTime endDate,
        double initialInvestment,
        double monthlyInvestment)
    {
        var filteredData = historicalData
            .Where(d => d.Date >= startDate && d.Date <= endDate)
            .OrderBy(d => d.Date)
            .ToList();

        if (!filteredData.Any())
        {
            Console.WriteLine("Žádná data pro zadané období.");
            return;
        }

        var monthlyData = filteredData
            .GroupBy(d => new { d.Date.Year, d.Date.Month })
            .Select(g => new
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                ClosePrice = g.Last().Close
            })
            .OrderBy(g => g.Year).ThenBy(g => g.Month)
            .ToList();

        double currentBalance = initialInvestment;
        double totalInvested = initialInvestment;
        double cumulativeInterest = 0;
        double totalInvestedToDate = initialInvestment;

        Console.WriteLine("--- Přehled po jednotlivých letech ---");

        int currentYear = monthlyData.First().Year;
        int monthsInYear = 0;
        double yearStartBalance = currentBalance;
        double yearInvested = initialInvestment;
        double? lastMonthPrice = monthlyData.First().ClosePrice;

        foreach (var month in monthlyData)
        {
            if (month.Year != currentYear && !lastMonthPrice.Equals(monthlyData.First().ClosePrice))
            {
                double yearlyInterest = currentBalance - yearStartBalance ;
                
                cumulativeInterest += yearlyInterest;

                double baseForReturn = yearStartBalance > 0 ? yearStartBalance : yearInvested;
                double yearReturn = baseForReturn > 0 ? (yearlyInterest / baseForReturn * 100) : 0;
                double totalReturn = totalInvestedToDate > 0 ? (cumulativeInterest / totalInvestedToDate * 100) : 0;

                Console.WriteLine($"Rok {currentYear}:");
                Console.WriteLine($"  Investováno tento rok: {yearInvested:C}");
                Console.WriteLine($"  Úrok za tento rok: {yearlyInterest:C}");
                Console.WriteLine($"  Zhodnocení za tento rok: {yearReturn:F2}%");
                Console.WriteLine($"  Celková hodnota portfolia: {currentBalance:C}");
                Console.WriteLine($"  Celkový úrok od začátku: {cumulativeInterest:C}");
                Console.WriteLine($"  Celkové zhodnocení: {totalReturn:F2}%");

                currentYear = month.Year;
                monthsInYear = 0;
                yearStartBalance = currentBalance;
                yearInvested = 0;
            }

            currentBalance += monthlyInvestment;
            totalInvested += monthlyInvestment;
            yearInvested += monthlyInvestment;
            monthsInYear++;
            totalInvestedToDate += monthlyInvestment;

            if (lastMonthPrice.HasValue)
            {
                double monthlyReturn = (month.ClosePrice - lastMonthPrice.Value) / lastMonthPrice.Value;
                currentBalance *= (1 + monthlyReturn);
            }
            
            lastMonthPrice = month.ClosePrice;

            if (month.Equals(monthlyData.Last()))
            {
                double yearlyInterest = currentBalance - yearStartBalance ;
                cumulativeInterest += yearlyInterest;

                double baseForReturn = yearStartBalance > 0 ? yearStartBalance : yearInvested;
                double yearReturn = baseForReturn > 0 ? (yearlyInterest / baseForReturn * 100) : 0;
                double totalReturn = totalInvestedToDate > 0 ? (cumulativeInterest / totalInvestedToDate * 100) : 0;

                Console.WriteLine($"Rok {currentYear}:");
                Console.WriteLine($"  Investováno tento rok: {yearInvested:C}");
                Console.WriteLine($"  Úrok za tento rok: {yearlyInterest:C}");
                Console.WriteLine($"  Zhodnocení za tento rok: {yearReturn:F2}%");
                Console.WriteLine($"  Celková hodnota portfolia: {currentBalance:C}");
                Console.WriteLine($"  Celkový úrok od začátku: {cumulativeInterest:C}");
                Console.WriteLine($"  Celkové zhodnocení: {totalReturn:F2}%");
            }
        }

        double totalGains = currentBalance - totalInvested;
        double finalTotalReturn = totalInvested > 0 ? (totalGains / totalInvested * 100) : 0;

        Console.WriteLine("\n--- Celkový přehled ---");
        Console.WriteLine($"Celková investovaná částka: {totalInvested:C}");
        Console.WriteLine($"Obdržený úrok: {totalGains:C}");
        Console.WriteLine($"Celkové zhodnocení: {finalTotalReturn:F2}%");
        Console.WriteLine($"Výsledná částka: {currentBalance:C}");
    }
}