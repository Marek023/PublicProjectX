using MarketIndexAnalyzer.Enums;
using MarketIndexAnalyzer.Models.Index;
using MarketIndexAnalyzer.Repositories.IndexRepositories.Interfaces;
using MarketIndexAnalyzer.Services.Calculations.Interfaces;

namespace MarketIndexAnalyzer.Services.Calculations.Implementations;

public class CalculationOfSlumps : ICalculationOfSlumps
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CalculationOfSlumps(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void GetResult()
    {
        Calculation();
    }

    private void Calculation()
    {
        var historicalData = GetHistoricalData();

        double investmentAmount = 100; // Zadaná částka
        double baseDropPercentage = 3; // Nastavitelný základní procentuální pokles
        double percentageStep = 10; // Kdy se částka zdvojnásobí 
        double currentDropThreshold = baseDropPercentage; // Aktuální práh pro investici
        double highestValue = 0; // Nejvyšší historická hodnota
        double totalInvested = 0; // Celkově investovaná částka
        double totalGains = 0; // Celkový výnos
        
        var investments = new List<(DateTime Time, double Drop, double Amount, double MaxDuringInvestment)>(); 

        
        var sortedRecords = historicalData.OrderBy(r => r.Time).ToList();

        foreach (var record in sortedRecords)
        {
            
            if (record.High > highestValue)
            {
                highestValue = record.High;
                currentDropThreshold = baseDropPercentage;
            }

            
            double drop = ((highestValue - record.High) / highestValue) * 100;

            while (drop >= currentDropThreshold)
            {
                
                int doublingFactor = (int)(drop / percentageStep); 
                double currentInvestment = investmentAmount * Math.Pow(2, doublingFactor);

              
                Console.WriteLine($"Investing {currentInvestment:C} at {record.Time.ToString("dd.MM.yyyy HH:mm")} with drop {drop:F2}%");
                
                investments.Add((record.Time, drop, currentInvestment,highestValue)); 
                totalInvested += currentInvestment;

               
                currentDropThreshold += baseDropPercentage;
            }
        }

       
        double finalPrice = sortedRecords.Last().Close; 
        totalGains = 0; 

        foreach (var investment in investments)
        {
           
            double purchasePrice = investment.MaxDuringInvestment * (1 - investment.Drop / 100);

            
            double growth = finalPrice - purchasePrice;
            double gain = (growth / purchasePrice) * investment.Amount;

            totalGains += gain;
        }

        
        double percentageGain = (totalGains / totalInvested) * 100;

        Console.WriteLine($"\nTotal invested over the year: {totalInvested:C}");
        Console.WriteLine($"Total gains over the year: {totalGains:C}");
        Console.WriteLine($"Percentage gain: {percentageGain:F2}%");
    }


    private List<IndexDataModel> GetHistoricalData()
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var indexDataRepository = scope.ServiceProvider.GetService<IIndexDataRepository>()
                                      ?? throw new NullReferenceException("indexDataRepository is null");

            var indexTypeRepository = scope.ServiceProvider.GetService<IIndexTypeRepository>()
                                      ?? throw new NullReferenceException("indexTypeRepository is null");

            var indexTypeModel = indexTypeRepository.GetIndexTypeById((int)IndexTypeEnum.Nasdaq);
            
            return indexDataRepository.GetAllDataByIndexTypeId(indexTypeModel.Id)
                .ToList();
        }
    }
}