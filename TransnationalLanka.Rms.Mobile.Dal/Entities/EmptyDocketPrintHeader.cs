using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("EmptyDocketPrintHeader")]
    public class EmptyDocketPrintHeader
    {
        [Key]
        [Column("serialNo")]
        public int SerialNo { get; set; }

        [Column("requestNo")]
        public string RequestNo { get; set; }

        [Column("printedOn")]
        public DateTime PrintedOn { get; set; }

    }
}
