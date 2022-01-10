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

            app.MapGet("/v1/api/locationSummaryByUser/{userName}", GetScanBySummary)
               .WithName("Get Location Summary by User");

            app.MapGet("/v1/api/locationDetailByUser/{userName}", GetScanByDetail)
            .WithName("Get Location Details by User");

            app.MapPost("/v1/api/locationItem", AddLocationItem)
               .WithName("Add Location Item");
        }

        public static async Task<IResult> GetByCode([FromRoute] string code,
            [FromServices] ILocationService locationService)
        {
            var location = await locationService.GetLocationByCode(code);
            return Results.Ok(location);
        }

        public static async Task<IResult> AddLocationItem(List<LocationItemDto> locationItem,
           [FromServices] ILocationService locationService)
        {
            var location = await locationService.AddLocationItem(locationItem);
            return Results.Ok(location);
        }

        public static IResult GetScanBySummary([FromRoute] string userName,
            [FromServices] ILocationService locationService)
        {
            var locationSummary = locationService.GetScanBySummary(userName);
            return Results.Ok(locationSummary);
        }

        public static async Task<IResult> GetScanByDetail([FromRoute] string userName, [FromQuery] DateTime dateUtc, [FromServices] ILocationService locationService,[FromQuery] string searchText = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var locationDetail = await locationService.GetScanByDetail(userName, dateUtc,searchText,pageIndex,pageSize);
            return Results.Ok(locationDetail);
        }
    }
}

