using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.MobileDevice
{
    public interface IMobileDeviceService
    {
        public Dal.Entities.MobileDevice GetMobielDevices(string deviceId);
    }
}
