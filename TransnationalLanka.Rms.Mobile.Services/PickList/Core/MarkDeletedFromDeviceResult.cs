using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.PickList.Core
{
    public class MarkDeletedFromDeviceResult
    {
        public long TrackingId { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }

}
