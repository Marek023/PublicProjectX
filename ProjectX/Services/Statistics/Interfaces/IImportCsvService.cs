namespace ProjectX.Services.Statistics.Interfaces;

public interface IImportCsvService
{
    void SaveAccountHistoryCsv(IFormFile accountHistory, IFormFile dividendHistory, IFormFile depositHistory, string nonTradingAmount);
}