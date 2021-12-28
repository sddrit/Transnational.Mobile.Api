using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.MetaData;

namespace TransnationalLanka.Rms.Mobile.WebApi.MetaData
{
    public static class MetaDataRoutes
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/metadata", GetMetaData)
                .WithName("Get Meta Data");

        }

        public static IResult GetMetaData([FromServices] IMetaDataService metaDataService)
        {
            var metaData = metaDataService.GetMetaData();
            return Results.Ok(metaData);
        }


    }
}
