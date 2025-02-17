using Microsoft.AspNetCore.Mvc;
using ProjectX.Repositories.Statistics.Interfaces;
using ProjectX.Services.Statistics.Interfaces;

namespace ProjectX.Controllers;

public class StatisticController : Controller
{
    private readonly  ILogger<StatisticController> _logger;
    private readonly IStatisticRepository _statisticRepository;
    private readonly IImportCsvService _importCsvService;

    public StatisticController(
        ILogger<StatisticController> logger,
        IStatisticRepository statisticRepository,
        IImportCsvService importCsvService)
    {
        _logger = logger;
        _statisticRepository = statisticRepository;
        _importCsvService = importCsvService;
    }

    [HttpGet]
    public IActionResult GetStatisticView()
    {
        try
        {
            var userId = Guid.Parse("087275bc-d442-4130-b4a6-d90cb2054334");
            var year = DateTime.Today.Year;
            
            var statisticModel = _statisticRepository.GetStatistic(userId, year);
            return View("/Views/Statistic/Statistic.cshtml", statisticModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetStatisticView");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> ImportCsv(
        IFormFile depositHistory,
        IFormFile accountHistory,
        IFormFile dividendHistory,
        string nonTradingAmount)
    {
        if (depositHistory == null || accountHistory == null || dividendHistory == null)
        {
            return BadRequest("Některé soubory chybí.");
        }

        _importCsvService.SaveAccountHistoryCsv(accountHistory, dividendHistory, depositHistory, nonTradingAmount);
        Console.WriteLine($"Neobchodovaná částka: {nonTradingAmount}");

        return Ok();
    }

}
