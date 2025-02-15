using System.Globalization;
using ProjectX.Models.Statistic;
using ProjectX.Services.Statistics.Interfaces;

namespace ProjectX.Services.Statistics.Implementations;

public class ImportCsvService : IImportCsvService
{
    public void SaveAccountHistoryCsv(
        IFormFile accountHistory,
        IFormFile dividendHistory,
        IFormFile depositHistory,
        string nonTradingAmount)
    {
        var accountHistoryModels = GetAccountHistoryModels(accountHistory);
        var dividendHistoryModels = GetCashOperationHistoryModels(dividendHistory);
        var depositHistoryModels = GetCashOperationHistoryModels(depositHistory);

        var monthStatisticModels = GetMonthStatisticModels(
            accountHistoryModels,
            dividendHistoryModels,
            depositHistoryModels,
            nonTradingAmount);

        // měsíční data jsou hotové ted se musi přepočítat po měsících a vložit do ročního modelu
        // pak spočítat i totalYear
        
        Console.WriteLine("xx");
        
    }

    private List<AccountHistoryModel> GetAccountHistoryModels(IFormFile accountHistory)
    {
        var rows = GetRowsFromCsvFile(accountHistory);

        var accountHistoryModels = new List<AccountHistoryModel>();
        foreach (var row in rows)
        {
            var part = row.Split(';');

            var accountHistoryModel = new AccountHistoryModel
            {
                Symbol = part[0],
                XtbId = part[1],
                Type = part[2],
                Volume = int.Parse(part[3]),
                OpenTime = DateTime.Parse(part[4]),
                OpenPrice = decimal.Parse(part[5], CultureInfo.InvariantCulture),
                CloseTime = DateTime.Parse(part[6]),
                ClosePrice = decimal.Parse(part[7], CultureInfo.InvariantCulture),
                Profit = decimal.Parse(part[8], CultureInfo.InvariantCulture),
            };
            accountHistoryModels.Add(accountHistoryModel);
        }

        return accountHistoryModels;
    }

    private List<AccountHistoryModel> GetCashOperationHistoryModels(IFormFile accountHistory)
    {
        var rows = GetRowsFromCsvFile(accountHistory);

        var accountHistoryModels = new List<AccountHistoryModel>();
        foreach (var row in rows)
        {
            var part = row.Split(';');

            var accountHistoryModel = new AccountHistoryModel
            {
                XtbId = part[0],
                Type = part[1],
                CloseTime = DateTime.Parse(part[2]),
                Symbol = part[3],
                Description = part[4],
                Profit = decimal.Parse(part[5], CultureInfo.InvariantCulture)
            };
            accountHistoryModels.Add(accountHistoryModel);
        }

        return accountHistoryModels;
    }

    private List<string> GetRowsFromCsvFile(IFormFile accountHistory)
    {
        var fileContent = GetContentFromCsvFile(accountHistory);

        return fileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .ToList();
    }

    private string GetContentFromCsvFile(IFormFile accountHistory)
    {
        using (var content = new StreamReader(accountHistory.OpenReadStream()))
        {
            return content.ReadToEnd();
        }
    }

    public List<MonthStatisticModel> GetMonthStatisticModels(
        List<AccountHistoryModel> accountHistoryModels,
        List<AccountHistoryModel> dividendHistoryModels,
        List<AccountHistoryModel> depositHistoryModels,
        string nonTradingAmount)
    {
        var dividendsWithDeposit = GetClosedPositions(dividendHistoryModels, depositHistoryModels, nonTradingAmount);
        var closedPositionsWithDeposit =
            GetClosedPositions(accountHistoryModels, depositHistoryModels, nonTradingAmount);

        closedPositionsWithDeposit.AddRange(dividendsWithDeposit);

        var monthStatisticModels = closedPositionsWithDeposit.OrderBy(data => data.CloseTime)
            .ToList();

        return monthStatisticModels;
    }


    private List<MonthStatisticModel> GetClosedPositions(
        List<AccountHistoryModel> closedPositions,
        List<AccountHistoryModel> depositHistoryModels,
        string nonTradingAmount)
    {
        var monthStatisticModels = new List<MonthStatisticModel>();

        foreach (var closedPosition in closedPositions)
        {
            var currentDeposit = GetCurrentTotalDeposit(closedPosition.CloseTime, depositHistoryModels, nonTradingAmount);

            var monthStatisticModel = new MonthStatisticModel
            {
                XtbId = closedPosition.XtbId,
                Symbol = closedPosition.Symbol,
                Type = closedPosition.Type,
                TotalDeposit = currentDeposit,
                Volume = closedPosition.Volume,
                OpenTime = closedPosition.OpenTime,
                OpenPrice = closedPosition.OpenPrice,
                CloseTime = closedPosition.CloseTime,
                ClosePrice = closedPosition.ClosePrice,
                Profit = closedPosition.Profit,
                TotalProfitInPercent = GetTotalProfitInPercent(closedPosition.Profit, currentDeposit)
            };

            monthStatisticModel.ProfitInPercent = closedPosition.Type.Contains("Dividenda")
                ? 0
                : GetProfitInPercent(closedPosition.OpenPrice, closedPosition.ClosePrice);

            monthStatisticModels.Add(monthStatisticModel);
        }

        return monthStatisticModels;
    }

    private decimal GetCurrentTotalDeposit(
        DateTime closeTime,
        List<AccountHistoryModel> depositHistoryModels,
        string nonTradingAmountString)
    {
        var lasTimeDataModels = depositHistoryModels.Where(deposit => deposit.CloseTime <= closeTime)
            .ToList();

        decimal totalDeposit = 0;
        foreach (var dataModel in lasTimeDataModels)
        {
            totalDeposit += dataModel.Profit;
        }
        
        var nonTradingAmount = !string.IsNullOrEmpty(nonTradingAmountString)
            ? decimal.Parse(nonTradingAmountString, CultureInfo.InvariantCulture)
            : 0;
        
        return totalDeposit - nonTradingAmount;
    }

    private decimal GetTotalProfitInPercent(decimal profit, decimal currentDeposit)
    {
        

        if (currentDeposit == 0)
            return 0;

        return profit / currentDeposit;
    }

    private decimal GetProfitInPercent(decimal openPrice, decimal closePrice)
    {
        if (openPrice == 0)
            return 0;

        return (closePrice - openPrice) / openPrice;
    }
}