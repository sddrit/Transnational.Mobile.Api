using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("UserPassword")]
    public class UserPassword
    {
        [Key]
        [Column("trackingId")]
        public long? TrackingId { get; set; }

        [Column("userId")]
        public int? UserId { get; set; }

        [Column("passwordHash")]
        public byte[]? PasswordHash { get; set; }

        [Column("passwordSalt")]
        public byte[]? PasswordSalt { get; set; }

        [Column("passwordExpiryDate", TypeName = "datetime")]
        public DateTime? PasswordExpiryDate { get; set; }

        [Column("passwordCreatedDate", TypeName = "datetime")]
        public DateTime? PasswordCreatedDate { get; set; }

    }
}
