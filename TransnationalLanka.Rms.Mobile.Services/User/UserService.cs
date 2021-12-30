using Microsoft.EntityFrameworkCore;
using System.Text;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Services.User.Core;

namespace TransnationalLanka.Rms.Mobile.Services.User
{
    public class UserService : IUserService
    {
        private readonly RmsDbContext _context;

        public UserService(RmsDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> GetUsersByUserName(string userName)
        {
            int userId;

            int.TryParse(userName, out userId);

            var user = await _context.Users
                   .Include(u => u.UserPasswords)
                   .Where(u => u.UserId == userId
                                 && u.Active
                                 && u.UserRoles.Any(r => r.Role.Active
                                 && (r.Role.Description == "Mobile User" || r.Role.Description == "Mobile Manager")
                                 && r.Role.Active)
                         )
                   .OrderBy(u => u.UserId)
                   .Select(u => new UserDto
                   {
                       Id = u.UserId,
                       FullName = u.UserFullName,
                       UserName = u.UserName,
                       Active = u.Active,
                       PasswordSalt = ByteArrayToString(u.UserPasswords.First().PasswordSalt),
                       PasswordHash = ByteArrayToString( u.UserPasswords.First().PasswordHash),
                       Roles = u.UserRoles.Select(r => r.Role.Description).ToList()
                   }).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ServiceException(string.Empty, $"Unable to find user by user name {userName}");
            }

            return user;
        }

        public List<UserDto> GetUsers()
        {
            var users = _context.Users
                  .Include(u => u.UserPasswords)
                  .Where(u => u.Active
                                && u.UserRoles.Any(r => r.Role.Active
                                && (r.Role.Description == "Mobile User" || r.Role.Description == "Mobile Manager")
                                && r.Role.Active)
                        )
                  .OrderBy(u => u.UserId)
                  .Select(u => new UserDto
                  {
                      Id = u.UserId,
                      FullName = u.UserFullName,
                      UserName = u.UserName,
                      Active = u.Active,
                      PasswordSalt = ByteArrayToString(u.UserPasswords.First().PasswordSalt),
                      PasswordHash = ByteArrayToString(u.UserPasswords.First().PasswordHash),
                      Roles = u.UserRoles.Select(r => r.Role.Description).ToList()
                  }).ToList();

            return users;
        }

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}
