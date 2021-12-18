using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("CartonStorage")]
    public class ItemStorage
    {

        [Key]
        [Column("cartonNo")]
        public int CartonNo { get; set; }

        [Column("customerId")]
        public int CustomerId { get; set; }

        [Column("locationCode")]
        [MaxLength(20)]
        public string? LocationCode { get; set; }       
       
        [Column("lastUpdateDate")]
        public int? LastUpdateDate { get; set; }

        [Column("lastScannedDateTime")]
        public DateTime? LastScannedDateTime { get; set; }

        [Column("lastScannedUser")]
        public string? LastScannedUserName { get; set; }


        

        
    }
}
