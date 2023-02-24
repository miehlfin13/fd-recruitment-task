using MediatR;
using Todo_App.Application.Colours.GetSupportedColours;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.WeatherForecasts.Queries.GetSupportedColour;

public record GetSupportedColourQuery : IRequest<IEnumerable<SupportedColour>>;

public class GetSupportedColourQueryHandler : RequestHandler<GetSupportedColourQuery, IEnumerable<SupportedColour>>
{
    protected override IEnumerable<SupportedColour> Handle(GetSupportedColourQuery request)
    {
        foreach (var colour in Colour.SupportedColours)
        {
            yield return new SupportedColour { Name = colour.Name, Value = colour.Code };
        }
    }
}
