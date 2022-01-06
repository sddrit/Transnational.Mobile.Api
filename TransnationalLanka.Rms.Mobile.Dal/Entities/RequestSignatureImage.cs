using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("RequestSignatureImage")] 
    public class RequestSignatureImage
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("requestNo")]
        [MaxLength(20)]
        public string RequestNo { get; set; }

        [Column("imagePath")]
        public string ImagePath { get; set; }

        [Column("contentType")]
        public string ContentType { get; set; }

        [Column("uploadedDate")]
        public DateTime UploadedDate { get; set; }

        [Column("uploadedBy")]
        public string UploadedBy { get; set; }

        [Column("docketSerialNo")]
        public long DocketSerialNo { get; set; }

        [Column("customerName")]
        public string CustomerName { get; set; }

        [Column("customerNIC")]
        public string CustomerNIC { get; set; }

        [Column("customerDesignation")]
        public string CustomerDesignation { get; set; }

        [Column("customerDepartment")]
        public string CustomerDepartment { get; set; }
        
    }
}
