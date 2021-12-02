using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.Location;

namespace TransnationalLanka.Rms.Mobile.WebApi.Location
{
    public static class LocationRoutes
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/api/location/{code}", GetByCode)
                .WithName("Get Location");
        }

        public static async Task<IResult> GetByCode([FromRoute]string code, 
            [FromServices] ILocationService locationService)
        {
            var location = await locationService.GetLocationByCode(code);
            return Results.Ok(location);
        }
    }
}
