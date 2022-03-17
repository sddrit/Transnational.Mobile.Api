using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.User.Core
{
    public  class UserLoginHistoryDto
    {
        public int UserId { get; set; }

        public DateTime LoginDate { get; set; }

        public string HostName { get; set; }

    }
}
