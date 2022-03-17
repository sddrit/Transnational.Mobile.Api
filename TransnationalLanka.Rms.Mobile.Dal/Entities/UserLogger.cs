using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    [Table("UserLogger")]
    public class UserLogger
    {
        [Key]
        [Column("trackingId")]
        public Int64 TrackingId { get; set; }

        [Column("userId")]
        public int UserId { get; set; }


        [Column("loginDate")]
        public DateTime LoginDate { get; set; }

        [Column("hostName")]
        public string HostName { get; set; }
        

    }
}
