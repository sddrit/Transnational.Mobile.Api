using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("RequestHeader")]
    public  class RequestHeader
    {
        [Key]
        [Column("requestNo")]
        [MaxLength(20)]
        public string RequestNo { get; set; }

        [Column("isDigitallySigned")]
        public bool? IsDigitallySigned { get; set; }

        [Column("digitallySignedDate")]
        public DateTime? DigitallySignedDate { get; set; }

        [Column("digitallySignedBy")]
        public string? DigitallySignedBy { get; set; }
      
    }
}
