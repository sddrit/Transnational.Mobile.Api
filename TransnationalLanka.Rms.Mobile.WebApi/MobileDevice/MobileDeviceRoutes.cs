using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.MobileDevice;

namespace TransnationalLanka.Rms.Mobile.WebApi.MobileDevice
{
    public class MobileDeviceRoutes
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/mobileDevice/{deviceUniqueId}", GetMobileDevices)
                .WithName("Get Mobile Device");

        }

        public static IResult GetMobileDevices([FromRoute] string deviceUniqueId, [FromServices] IMobileDeviceService mobileDataService)
        {
            var mobileDevice = mobileDataService.GetMobielDevices(deviceUniqueId);
            return Results.Ok(mobileDevice);
        }
    }
}
