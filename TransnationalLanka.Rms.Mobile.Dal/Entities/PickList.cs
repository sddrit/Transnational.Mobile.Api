using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("PickList")]
    public  class PickList
    {
        [Key]
        [Column("trackingId")]
        public long TrackingId { get; set; }

        [Required]
        [Column("pickListNo")]
        [MaxLength(50)]
        public string PickListNo { get; set; }

        [Column("cartonNo")]
        public int CartonNo { get; set; }
     
        [Column("barcode")]
        [MaxLength(50)]
        public string Barcode { get; set; }

        [Column("locationCode")]
        [MaxLength(20)]
        public string LocationCode { get; set; }

        [Column("wareHouseCode")]
        [MaxLength(20)]
        public string WareHouseCode { get; set; }

        [Column("lastSentDeviceId")]
        [MaxLength(50)]
        public string LastSentDeviceId { get; set; }

        [Column("assignedUserId")]
        public int? AssignedUserId { get; set; }

        [Column("requestNo")]
        [MaxLength(20)]
        public string RequestNo { get; set; }

        [Column("pickedUserId")]
        public int PickedUserId { get; set; }

        [Column("isPicked")]
        public bool IsPicked { get; set; }

        [Column("pickDate")]
        public long? PickDate { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("createdUserId")]
        public int? CreatedUserId { get; set; }

        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column("luUserID")]
        public int? LuUserId { get; set; }

        [Column("luDate", TypeName = "datetime")]
        public DateTime? LuDate { get; set; }

        [Column("deleted")]
        public bool? Deleted { get; set; }

    }


    
}
