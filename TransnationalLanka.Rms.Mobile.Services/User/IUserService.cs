using TransnationalLanka.Rms.Mobile.Services.User.Core;

namespace TransnationalLanka.Rms.Mobile.Services.User
{
    public interface IUserService
    {
        public List<UserDto> GetUsers();
        public Task<UserDto> GetUsersByUserName(string userName);
    }
}
