using ProjectX.Models.Assets;
using ProjectX.Repositories.Assets.Interfaces;
using ProjectX.Services.Calculators.Interfaces;

namespace ProjectX.Services.Calculators.Implementations;

public class CalculationOfSlumpsFromHistoricalDataService : ICalculationOfSlumpsFromHistoricalDataService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CalculationOfSlumpsFromHistoricalDataService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void GetResult()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var historicalDataRepository = scope.ServiceProvider.GetService<IAssetHistoricalDataRepository>()
                                           ?? throw new NullReferenceException("AssetHistoricalDataRepository is null");

            var historicalDataModels = historicalDataRepository.GetHistoricalDataByAssetId(1);
            var endDate = DateTime.Now;
            var startDate = endDate.AddYears(-10);

            var investmentAmount = 833; // Zadaná částka
            var baseDropPercentage = 2; // Nastavitelný základní procentuální pokles
            var percentageStep = 6; // Kdy se částka zdvojnásobí 


            Calculation(
                historicalDataModels,
                startDate,
                endDate,
                investmentAmount,
                baseDropPercentage,
                percentageStep);
        }
    }

    private void Calculation(List<AssetHistoricalDataModel> historicalDataModels,
        DateTime startDate,
        DateTime endDate,
        double investmentAmount,
        double baseDropPercentage,
        double percentageStep)
    {
        var sortedHistoricalData = historicalDataModels.Where(d => d.Date >= startDate && d.Date <= endDate)
            .OrderBy(d => d.Date)
            .ToList();

        double currentDropThreshold = baseDropPercentage; // Aktuální práh pro investici
        double highestValue = 0; // Nejvyšší historická hodnota
        double totalInvested = 0; // Celkově investovaná částka
        double totalGains = 0; // Celkový výnos

        var investments = new List<(DateTime Time, double Drop, double Amount, double MaxDuringInvestment)>();

        foreach (var record in sortedHistoricalData)
        {
           
            if (record.High > highestValue)
            {
                highestValue = record.High;
                currentDropThreshold = baseDropPercentage; 
                Console.WriteLine($"New historical high: {highestValue} on {record.Date.ToShortDateString()}");
            }

          
            double drop = ((highestValue - record.High) / highestValue) * 100;
           
            while (drop >= currentDropThreshold)
            {
               
                int doublingFactor = (int)(drop / percentageStep);
                double currentInvestment = investmentAmount * Math.Pow(2, doublingFactor);

                
                Console.WriteLine(
                    $"Investing {currentInvestment:C} at {record.Date.ToString("dd.MM.yyyy HH:mm")} with drop {drop:F2}%");

                investments.Add((record.Date, drop, currentInvestment,
                    highestValue));
                totalInvested += currentInvestment;

                
                currentDropThreshold += baseDropPercentage;
            }
        }
     
        double finalPrice = sortedHistoricalData.Last().Close;
        totalGains = 0;

        foreach (var investment in investments)
        {
          
            double purchasePrice = investment.MaxDuringInvestment * (1 - investment.Drop / 100);

            
            double growth = finalPrice - purchasePrice;
            double gain = (growth / purchasePrice) * investment.Amount;

            totalGains += gain;
        }

        
        double percentageGain = (totalGains / totalInvested) * 100;

        var currentBalance = totalGains + totalInvested;


        Console.WriteLine("\n--- Celkový přehled ---");
        Console.WriteLine($"Celková investovaná částka: {totalInvested:C}");
        Console.WriteLine($"Obdržený úrok: {totalGains:C}");
        Console.WriteLine($"Celkové zhodnocení: {percentageGain:F2}%");
        Console.WriteLine($"Výsledná částka: {currentBalance:C}");
    }


   
}