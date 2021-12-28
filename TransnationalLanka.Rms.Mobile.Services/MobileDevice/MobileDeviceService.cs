using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;

namespace TransnationalLanka.Rms.Mobile.Services.MobileDevice
{
    public class MobileDeviceService:IMobileDeviceService
    {
        private readonly RmsDbContext _context;

        public MobileDeviceService(RmsDbContext context)
        {
            _context = context;
        }

        public Dal.Entities.MobileDevice GetMobielDevices(string deviceId)
        {   
            var mobileDevice= _context.MobileDevices.Where(m=>m.Code.ToLower()==deviceId.ToLower()).FirstOrDefault();   

            if (mobileDevice == null)
            {
                throw new ServiceException(string.Empty, $"Unable to find device by id {deviceId}");
            }
            
            return mobileDevice;
        }
    }
}
