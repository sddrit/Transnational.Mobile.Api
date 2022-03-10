using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("RequestDetail")]
    public class RequestDetail
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }

        [Required]
        [Column("requestNo")]
        [MaxLength(20)]
        public string RequestNo { get; set; }

        [Column("cartonNo")]
        public int CartonNo { get; set; }

        [Column("fromMobile")]
        public bool? FromMobile { get; set; }

        [Column("picked")]
        public bool? Picked { get; set; }

        [Column("pickListNo")]
        [MaxLength(50)]
        public string? PickListNo { get; set; }

        [Column("deleted")]
        public bool? Deleted { get; set; }
        
        [Column("collectedDate")]
        public DateTime? CollectedDate { get; set; }

        [Column("collectedBy")]
        public string? CollectedBy { get; set; }        


    }
}
