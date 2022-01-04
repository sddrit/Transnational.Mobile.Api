using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("DocketPrintSlice")]
    public class DocketPrintSlice
    {
        [Key]
        [Column("trackingId")]
        public int TrackingId { get; set; }

        [Column("serialNo")]
        public int SerialNo { get; set; }

        [Column("requestNo")]
        public string RequestNo { get; set; }



    }
}
