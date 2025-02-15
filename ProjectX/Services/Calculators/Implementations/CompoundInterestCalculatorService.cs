using ProjectX.Services.Calculators.Interfaces;

namespace ProjectX.Services.Calculators.Implementations;

public class CompoundInterestCalculatorService : ICompoundInterestCalculatorService
{

    public void GetResult()
    {
        Calculation();
    }

    private void Calculation()
    {
        Console.Write("Zadejte počáteční investovanou částku (Kč): ");
        double initialInvestment = 100000;

        Console.Write("Zadejte pravidelnou měsíční investici (Kč): ");
        double monthlyInvestment = 20000;

        Console.Write("Zadejte předpokládanou úrokovou sazbu (% ročně): ");
        double annualInterestRate = double.Parse("18") / 100;

        Console.Write("Zadejte počet let spoření: ");
        int years = 10;

        double totalInvested = initialInvestment;
        double totalInterestEarned = 0;
        double currentBalance = initialInvestment;

        List<(int Year, double InvestedAmount, double InterestEarned)> yearlyBreakdown = new List<(int, double, double)>();

        
        for (int year = 1; year <= years; year++)
        {
            double yearlyInvested = monthlyInvestment * 12;
            double interestEarned = 0;

            for (int month = 1; month <= 12; month++)
            {
                double monthlyInterest = currentBalance * (annualInterestRate / 12);
                interestEarned += monthlyInterest;

                currentBalance += monthlyInvestment + monthlyInterest;
            }

            totalInvested += yearlyInvested;
            totalInterestEarned += interestEarned;

            yearlyBreakdown.Add((year, totalInvested, totalInterestEarned));
        }

        Console.WriteLine("\n--- Celkový přehled ---");
        Console.WriteLine($"Celková investovaná částka: {totalInvested:C}");
        Console.WriteLine($"Obdržený úrok: {totalInterestEarned:C}");
        Console.WriteLine($"Výsledná částka: {currentBalance:C}");

        Console.WriteLine("\n--- Přehled po jednotlivých letech ---");
        foreach (var breakdown in yearlyBreakdown)
        {
            Console.WriteLine($"Rok {breakdown.Year}: Investováno: {breakdown.InvestedAmount:C}, Úrok: {breakdown.InterestEarned:C}");
        }
    }
    
}