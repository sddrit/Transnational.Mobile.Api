using Microsoft.AspNetCore.Mvc;
using TransnationalLanka.Rms.Mobile.Services.User;
using TransnationalLanka.Rms.Mobile.Services.User.Core;

namespace TransnationalLanka.Rms.Mobile.WebApi.User
{
    public class UserRoutes
    {

        public static void Register(WebApplication app)
        {
            app.MapGet("/v1/api/user/{userName}", GetByUserName)
                .WithName("Get User By User Name");

            app.MapGet("/v1/api/users", GetUsres)
                .WithName("Get Users");

            app.MapPost("/v1/api/users/addUserLoginHistory", AddUserLoginHistory)
           .WithName("Update Login History");

        }

        public static async Task<IResult> GetByUserName([FromRoute] string userName,
            [FromServices] IUserService userService)
        {
            var user = await userService.GetUsersByUserName(userName);
            return Results.Ok(user);
            
        }

        public static IResult GetUsres([FromServices] IUserService userService)
        {
            var users = userService.GetUsers();
            return Results.Ok(users);
        }

        public static async Task<IResult> AddUserLoginHistory(UserLoginHistoryDto userLoginHistory,
         [FromServices] IUserService userService)
        {
            var pickListItems = await userService.AddUserLoginHistory(userLoginHistory);
            return Results.Ok(pickListItems);
        }
    }
}
