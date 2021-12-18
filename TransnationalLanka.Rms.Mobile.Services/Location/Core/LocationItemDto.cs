using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.Location.Core
{
    public  class LocationItemDto
    {
     
        public string BarCode { get; set; }
        public string LocationCode { get; set; }
        public string StorageType { get; set; }
        public string ScannedUserName { get; set; }
        public DateTime ScannedDateTime { get; set; }
    }
}
