using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.PickList;
using TransnationalLanka.Rms.Mobile.Services.PickList.Core;

namespace TransnationalLanka.Rms.Mobile.WebApi.PickList
{
    public class PickListRoutes
    {

        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/picklist/{deviceId}", GetPickListByDeviceId)
                .WithName("Get PickList By Device Id");


            app.MapPost("/v1/api/pickList", UpdatePickedStatus)
               .WithName("Update Picked Items");

            app.MapPost("/v1/api/pickList/markAsDeletedFromDevice", MarkAsDeleteFromDevice)
              .WithName("Mark As Delete From Device");
        }

        public static  IResult GetPickListByDeviceId([FromRoute] string deviceId,
            [FromServices] IPickListService pickListService)
        {
            var pickLists =  pickListService.GetPickLists(deviceId);
            return Results.Ok(pickLists);

        }

        public static async Task<IResult> UpdatePickedStatus(List<PickListInsertDto> pickListItem,
          [FromServices] IPickListService pickListService)
        {
            var pickListItems = await pickListService.UpdatePickStatus(pickListItem);
            return Results.Ok(pickListItems);
        }

        public static async Task<IResult> MarkAsDeleteFromDevice(PickListMarkDeleteDto pickListItem,
        [FromServices] IPickListService pickListService)
        {
            var pickListItems = await pickListService.MarkAsDeletedFromDevice(pickListItem);
            return Results.Ok(pickListItems);
        }
    }
}
