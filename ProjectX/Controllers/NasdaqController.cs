using Microsoft.AspNetCore.Mvc;
using ProjectX.Services.Calculators.Interfaces;
using ProjectX.Services.Nasdaq.Interfaces;
using ProjectX.Services.Sp500.Interfaces;

namespace ProjectX.Controllers;

public class NasdaqController : Controller
{
    private readonly ILogger<NasdaqController> _logger;
    private readonly INasdaqService _nasdaqService;

    public NasdaqController(ILogger<NasdaqController> logger, INasdaqService nasdaqService)
    {
        _logger = logger;
        _nasdaqService = nasdaqService; 
    }

    [HttpGet]
    public IActionResult GetNasdaqView()
    {
        try
        {
            var marketDataModels = _nasdaqService.GetDataForTable();
            return View("/Views/Nasdaq/Nasdaq.cshtml", marketDataModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Nasdaq");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        
    }
}