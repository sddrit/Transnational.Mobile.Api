using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.Location;
using TransnationalLanka.Rms.Mobile.Services.Location.Core;

namespace TransnationalLanka.Rms.Mobile.WebApi.Location
{
    public static class LocationRoutes
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/location/{code}", GetByCode)
                .WithName("Get Location");

            app.MapPost("/v1/api/locationItem", AddLocationItem)
               .WithName("Add Location Item");
        }

        public static async Task<IResult> GetByCode([FromRoute]string code, 
            [FromServices] ILocationService locationService)
        {
            var location = await locationService.GetLocationByCode(code);
            return Results.Ok(location);
        }

        public static async Task<IResult> AddLocationItem(List<LocationItemDto> locationItem,
           [FromServices] ILocationService locationService)
        {
            var location =  await locationService.AddLocationItem(locationItem);
            return Results.Ok(location);
        }
    }
}

