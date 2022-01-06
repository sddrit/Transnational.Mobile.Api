using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransnationalLanka.Rms.Mobile.Dal.Entities;

namespace TransnationalLanka.Rms.Mobile.Services.Request
{
    public  class CollectionCartonBindModel
    {
        public string RequestNo { get; set; }
        public string UserName  { get; set; }
        public List<DocketDetail> DocketDetails { get; set; }
    }
}
