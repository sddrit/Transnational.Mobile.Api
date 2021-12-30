using System.ComponentModel.DataAnnotations;


namespace TransnationalLanka.Rms.Mobile.Services.User.Core
{
    public class UserDto
    {
        [Key]

        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserNameMobile { get { return String.Format("{0:000}", Id); } set { } }

        public string FullName { get; set; }

        public bool Active { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public List<string> Roles { get; set; }
    }
}
