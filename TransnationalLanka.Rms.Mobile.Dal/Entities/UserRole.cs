using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("UserRole")]

    public partial class UserRole
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }

        [Key]
        [Column("roleId")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

    }
}
