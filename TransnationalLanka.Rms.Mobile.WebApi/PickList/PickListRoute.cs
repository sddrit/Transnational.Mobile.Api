using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.PickList;
using TransnationalLanka.Rms.Mobile.Services.PickList.Core;

namespace TransnationalLanka.Rms.Mobile.WebApi.PickList
{
    public class PickListRoute
    {

        public static void Register(WebApplication app)
        {
            app.MapGet("/api/picklist/{deviceId}", GetPickListByDeviceId)
                .WithName("Get PickList By Device Id");


            app.MapPost("/api/pickList", UpdatePickedStatus)
               .WithName("Update Picked Items");
        }

        public static  IResult GetPickListByDeviceId([FromRoute] string deviceId,
            [FromServices] IPickListService pickListService)
        {
            var pickLists =  pickListService.GetPickLists(deviceId);
            return Results.Ok(pickLists);

        }

        public static IResult UpdatePickedStatus(List<PickListDto> pickListItem,
          [FromServices] IPickListService pickListService)
        {
            var pickListItems = pickListService.UpdatePickStatus(pickListItem);
            return Results.Ok(pickListItems);
        }
    }
}
