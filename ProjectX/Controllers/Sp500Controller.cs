using Microsoft.AspNetCore.Mvc;
using ProjectX.Services.Sp500.Interfaces;

namespace ProjectX.Controllers;

public class Sp500Controller : Controller
{
    private readonly ILogger<Sp500Controller> _logger;
    private readonly ISp500Service _sp500Service;

    public Sp500Controller(ILogger<Sp500Controller> logger, ISp500Service sp500Service)
    {
        _logger = logger;
        _sp500Service = sp500Service;
    }

    [HttpGet]
    public IActionResult GetSp500View()
    {
        try
        {
            var marketDataModels = _sp500Service.GetDataForTable();
            return View("/Views/Sp500/Sp500.cshtml", marketDataModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Sp500");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}