using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("Customer")]
    public  class Customer
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }        
       
        [Column("customerCode")]
        [StringLength(20)]
        public string CustomerCode { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
