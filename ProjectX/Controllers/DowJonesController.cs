using Microsoft.AspNetCore.Mvc;
using ProjectX.Services.DowJones.Interfaces;

namespace ProjectX.Controllers;

public class DowJonesController : Controller
{
    private readonly ILogger<DowJonesController> _logger;
    private readonly IDowJonesService _dowJonesService;

    public DowJonesController(ILogger<DowJonesController> logger, IDowJonesService dowJonesService)
    {
        _logger = logger;
        _dowJonesService = dowJonesService;
    }

    [HttpGet]
    public IActionResult GetDowJonesView()
    {
        try
        {
            var marketDataModels = _dowJonesService.GetDataForTable();
            return View("/Views/DowJones/DowJones.cshtml", marketDataModels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Sp500");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}