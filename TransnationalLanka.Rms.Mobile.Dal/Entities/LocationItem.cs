using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("CartonLocation")]
    
    public class LocationItem
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
       
        [Column("cartonNo")]
        public int CartonNo { get; set; }

        [Column("barCode")]
        [MaxLength(50)]
        public string? BarCode { get; set; }

        [Column("locationCode")]
        [MaxLength(20)]
        
        public string LocationCode { get; set; }

        [Column("storageType")]
        [MaxLength(50)]
        public string StorageType { get; set; }

        [Column("isFromMobile")]
        public bool? IsFromMobile { get; set; }

        [Column("scannedDate")]
        public long? ScannedDateInt { get; set; }

        [Column("customerId")]
        public int? CustomerId { get; set; }

        [Column("scanDateTime")]
        [MaxLength(50)]
        public DateTime ScanDateTime { get; set; }
        
        [Column("createdUserId")]
        public string CreatedUserName { get; set; }

        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("luUserId")]
        public int? LuUserId { get; set; }

        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }
    }
}
