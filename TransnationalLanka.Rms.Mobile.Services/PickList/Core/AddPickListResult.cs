using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.PickList.Core
{
    public class AddPickListResult
    {
        public string PickListNo { get; set; }
        public int CartonNo { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
