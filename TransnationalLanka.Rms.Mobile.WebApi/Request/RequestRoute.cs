using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.Request;
using TransnationalLanka.Rms.Mobile.Services.Request.Core;

namespace TransnationalLanka.Rms.Mobile.WebApi.Request
{
    public class RequestRoute
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/request/{requestNo}/{userName}", GetDocket)
                .WithName("Get docket");

            app.MapGet("/v1/api/request/search/{requestNo}/{customerName}", SearchRequest)
                .WithName("Search request");

            app.MapGet("/v1/api/request/validate/{requestNo}/{cartonNo}", ValidateCartonsInRequest)
              .WithName("Validate carton");

            app.MapPost("/v1/api/request", UploadSignature)
            .WithName("Upload Signature");

        }

        public static async Task<IResult> GetDocket([FromRoute] string requestNo,string userName,
          [FromServices] IRequestService requestService)
        {
            var docket =await  requestService.GetDocketDetails(requestNo, userName);
            return Results.Ok(docket);

        }

        public static async Task<IResult> SearchRequest([FromRoute] string requestNo, string customerName,
        [FromServices] IRequestService requestService)
        {
            var request = await requestService.SearchRequestHeader(requestNo, customerName);
            return Results.Ok(request);

        }

       public static async Task<IResult> ValidateCartonsInRequest([FromRoute] string requestNo, int cartonNo,
       [FromServices] IRequestService requestService)
        {
            var request = await requestService.ValidateRequest(requestNo, cartonNo);
            return Results.Ok(request);

        }

        public static async Task<IResult> UploadSignature(RequestSignatureModel model,
       [FromServices] IRequestService requestService)
        {
            var uploadRequest = await requestService.UploadSignature(model);
            return Results.Ok(uploadRequest);
        }
    }
}
