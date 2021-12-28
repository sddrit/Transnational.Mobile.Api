using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.MobileDevice;

namespace TransnationalLanka.Rms.Mobile.WebApi.MobileDevice
{
    public class MobileDeviceRoutes
    {
        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/mobileDevice/{deviceId}", GetMobileDevices)
                .WithName("Get Mobile Device");

        }

        public static IResult GetMobileDevices([FromRoute] string deviceId, [FromServices] IMobileDeviceService mobileDataService)
        {
            var mobileDevice = mobileDataService.GetMobielDevices(deviceId);
            return Results.Ok(mobileDevice);
        }
    }
}
