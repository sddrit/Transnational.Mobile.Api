using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("User")]
    public partial class User
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }

        [Column("userName")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Column("userFullName")]
        public string UserFullName { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<UserPassword> UserPasswords { get; set; }

    }
}
