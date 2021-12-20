using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;

namespace TransnationalLanka.Rms.Mobile.Services.User
{
    public class UserService:IUserService
    {
        private readonly RmsDbContext _context;

        public UserService(RmsDbContext context)
        {
            _context = context;
        }

        public Dal.Entities.User GetUserById(string userName)
        {

            var mobileUser = _context.UserMobiles.Where(x => x.UserName == userName).FirstOrDefault();

            if (mobileUser == null)
            {
                throw new ServiceException(string.Empty, $"Unable to find user by {userName}");
            }

            var generalUser = _context.UserGenerals.Where(x => x.UserId == mobileUser.UserId
                                                                && x.Deleted == false).FirstOrDefault();

            var userPassword = _context.UserPasswords.Where(x => x.UserId == Convert.ToInt32(userName)).FirstOrDefault();


            return new Dal.Entities.User()
            {
                UserName = mobileUser.UserName,
                FullName = generalUser.FullName,
                UserType = mobileUser.UserType,
                Id = mobileUser.Id,
                Active = generalUser.Active,
                PasswordHash = userPassword.PasswordHash,
                PasswordSalt = userPassword.PasswordSalt


            };
        }


        public List<Dal.Entities.User> GetUsers()
        {
            var users = (
                from UM in _context.UserMobiles
                join UG in _context.UserGenerals on UM.UserId equals UG.UserId           

                join UP in _context.UserPasswords on UM.UserId equals UP.UserId
                

                select new Dal.Entities.User
                {
                    UserName = UM.UserName,
                    FullName = UG.FullName,
                    UserType = UM.UserType,
                    Id = UM.Id,
                    Active = UG.Active,
                    PasswordHash = UP.PasswordHash,
                    PasswordSalt = UP.PasswordSalt

                }).OrderBy(x => x.Id).ToList();

            return users;
        }
    }
}
