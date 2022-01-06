using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.Image;
using TransnationalLanka.Rms.Mobile.Services.Request;
using TransnationalLanka.Rms.Mobile.Services.Request.Core;
using TransnationalLanka.Rms.Mobile.WebApi.Models;

namespace TransnationalLanka.Rms.Mobile.WebApi.Request
{
    public class RequestRoute
    {
        public static void Register(WebApplication app)
        {
            app.MapPost("/v1/api/request/create-docket", CreateDocket)
                .WithName("Get docket");

            app.MapGet("/v1/api/request", SearchRequest)
                .WithName("Search request");

            app.MapGet("/v1/api/request/validate/{requestNo}/{cartonNo}", ValidateCartonsInRequest)
                .WithName("Validate carton");
        }

        public static async Task<IResult> CreateDocket([FromBody] CreateDocketBindingModel model,
            [FromServices] IRequestService requestService)
        {
            var docket = await requestService.GetDocketDetails(model.RequestNumber, model.UserName);
            return Results.Ok(docket);
        }

        public static async Task<IResult> SearchRequest([FromServices] IRequestService requestService,
            [FromQuery] string searchText = null,
            [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var request = await requestService.SearchRequestHeader(searchText, pageIndex, pageSize);
            return Results.Ok(request);
        }

        public static async Task<IResult> ValidateCartonsInRequest([FromRoute] string requestNo,
            [FromRoute] int cartonNo,
            [FromServices] IRequestService requestService)
        {
            var request = await requestService.ValidateRequest(requestNo, cartonNo);
            return Results.Ok(request);
        }
    }
}