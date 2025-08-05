using Microsoft.AspNetCore.Mvc;
using temperature_api.Dto;

namespace temperature_api.Controllers;

[ApiController]
[Route("[controller]")]
public class TemperatureController : ControllerBase
{
    private readonly ILogger<TemperatureController> _logger;
    private static readonly Random Random = new();

    public TemperatureController(ILogger<TemperatureController> logger)
    {
        _logger = logger;
    }


    [HttpGet(Name = "Get temperature by Location")]
    public TemperatureResponse GetByLocation([FromQuery] string location) =>
        new (location: location, sensorId: null, GetNumber());

    [HttpGet("{id}", Name = "Get temperature by Id")]
    public TemperatureResponse GetBySensorId([FromRoute] string id) =>
        new (location: null, sensorId: id, GetNumber());

    private static int GetNumber()
    {
        return Random.Next(-10, 40);
    }
}