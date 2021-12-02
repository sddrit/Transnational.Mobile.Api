using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("Location")]
    public class Location
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [MaxLength(20)]
        public string Code { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("name")]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
