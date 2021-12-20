using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.User;

namespace TransnationalLanka.Rms.Mobile.WebApi.User
{
    public class UserRoutes
    {

        public static void Register(WebApplication app)
        {
            app.MapGet("/api/user/{userName}", GetByUserId)
                .WithName("Get User By Id");

            app.MapGet("/api/users", GetUsres)
                .WithName("Get Users");
        }

        public static async Task<IResult> GetByUserId([FromRoute] string userName,
            [FromServices] IUserService userService)
        {
            var user = userService.GetUserById(userName);
            return Results.Ok(user);
        }

        public static IResult GetUsres([FromServices] IUserService userService)
        {
            var users = userService.GetUsers();
            return Results.Ok(users);
        }
    }
}
