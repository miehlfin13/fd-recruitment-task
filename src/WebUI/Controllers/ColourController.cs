using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.Colours.GetSupportedColours;
using Todo_App.Application.WeatherForecasts.Queries.GetSupportedColour;

namespace Todo_App.WebUI.Controllers;

public class ColourController : ApiControllerBase
{
    [HttpGet("Supported")]
    public async Task<IEnumerable<SupportedColour>> GetSupported()
    {
        return await Mediator.Send(new GetSupportedColourQuery());
    }
}
