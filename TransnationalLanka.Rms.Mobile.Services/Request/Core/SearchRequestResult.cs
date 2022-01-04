using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.Request.Core
{
    public class SearchRequestResult
    {
        public string RequestNo { get; set; }
        public string Name { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDigitallySigned { get; set; }

    }
}
