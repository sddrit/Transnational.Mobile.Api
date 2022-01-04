using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("ViewRequestSummary")]
    public class RequestView
    {

        [Key]
        [MaxLength(20)]
        [Column("Request No")]
        public string RequestNo { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Customer Code")]
        public string CustomerCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Column("Delivery Date")]
        public int? DeliveryDate { get; set; }

        [MaxLength(50)]
        [Column("Order Received By")]
        public string OrderReceivedBy { get; set; }

        [MaxLength(50)]
        [Column("Customer Reference")]
        public string CustomerReference { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Authorized Officer")]
        public string AuthorizedOfficer { get; set; }

        [Column("No Of Cartons")]
        public int? NoOfCartons { get; set; }

        [MaxLength(10)]
        [Column("Request Type")]
        public string RequestType { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

        [MaxLength(100)]
        [Column("Storage Type")]
        public string StorageType { get; set; }

        [MaxLength(20)]
        [Column("Wo Type")]
        public string WoType { get; set; }

        [Column("Created User")]
        [MaxLength(100)]
        public string CreatedUser { get; set; }

        [Column("Created Date")]
        [MaxLength(4000)]
        public string CreatedDate { get; set; }

        [Column("Created Date Time")]
        [MaxLength(4000)]
        public string CreatedDateTime { get; set; }

        [Required]
        [Column("Lu User")]
        [MaxLength(100)]
        public string LuUser { get; set; }

        [Column("Lu Date")]
        [MaxLength(4000)]
        public string LuDate { get; set; }

        [Column("Lu Date Time")]
        [MaxLength(4000)]
        public string LuDateTime { get; set; }

        [MaxLength(50)]
        [Column("Delivery Route")]
        public string DeliveryRoute { get; set; }

        [MaxLength(50)]
        [Column("Service Type")]
        public string? ServiceType { get; set; }

        [Column("Docket No")]
        [MaxLength(200)]
        public string DocketNo { get; set; }

        [Column("Department")]
        public string Department { get; set; }

        [Column("Po No")]
        public string PoNo { get; set; }

        [Column("Contact Person")]
        public string ContactPerson { get; set; }

        [Column("Contact No")]
        public string ContactNo { get; set; }

        [Column("Docket Type")]
        public string DocketType { get; set; }

        [Column("Is PrintAlternative No")]
        public bool IsPrintAlternativeNo { get; set; }

        [Column("Route")]
        public string Route { get; set; }

        [Column("Last Confirmed Date")]
        public int? LastConfirmedDate { get; set; }
    }
}
