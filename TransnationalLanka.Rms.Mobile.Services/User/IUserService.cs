using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.User
{
    public interface IUserService
    {
        public List<Dal.Entities.User> GetUsers();
        public Dal.Entities.User GetUserById(string userName);
    }
}
