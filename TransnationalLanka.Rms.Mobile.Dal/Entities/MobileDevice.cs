using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("MobileDevice")]
    public class MobileDevice
    {
        [Key]
        [Column("code")]
        [MaxLength(20)]
        public string Code { get; set; }

        [Column("id")]
        public int Id { get; set; }       

        [Column("deviceUniqueId")]
        public string? DeviceUniqueId { get; set; }

        [Column("description")]
        [MaxLength(50)]
        public string Description { get; set; }

        [Column("active")]
        public bool? Active { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
