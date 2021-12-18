using GuardNet;
using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Core.Extensions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Dal.Entities;
using TransnationalLanka.Rms.Mobile.Services.Location.Core;

namespace TransnationalLanka.Rms.Mobile.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly RmsDbContext _context;

        public LocationService(RmsDbContext context)
        {
            _context = context;
        }

        public async Task<LocationDto> GetLocationByCode(string code)
        {
            Guard.NotNullOrEmpty("Location Code", nameof(code));

            var location = await
                _context.Locations.FirstOrDefaultAsync(l => l.Code.ToLower() == code.ToLower());

            if (location == null)
            {
                throw new ServiceException(string.Empty, $"Unable to find location from code number {code}");
            }

            return new LocationDto()
            {
                Code = location.Code,
                Active = location.Active,
                Id = location.Id,
                Name = location.Name,
                Type = location.Type


            };
        }


        public bool AddLocationItem(List<LocationItemDto> locationItems)
        {

            foreach (var locationItem in locationItems)
            {
                Guard.NotNullOrEmpty("BarCode", nameof(locationItem.BarCode));
                Guard.NotNullOrEmpty("Location Code", nameof(locationItem.LocationCode));
                Guard.NotNullOrEmpty("Scanned DateTime", nameof(locationItem.ScannedDateTime));

                int cartonNo;
                bool isNumeric = int.TryParse(locationItem.BarCode, out cartonNo);

                var itemStorage = _context.ItemStorages.Where(x => x.CartonNo == cartonNo).FirstOrDefault();

                var customerCode = itemStorage != null ? _context.Customers.Where(x => x.TrackingId == itemStorage.CustomerId).FirstOrDefault().CustomerCode : "0";

                var item = new LocationItem()
                {

                    CartonNo = isNumeric ? cartonNo : 0,
                    BarCode = locationItem.BarCode,
                    LocationCode = locationItem.LocationCode,
                    ScanDateTime = locationItem.ScannedDateTime,
                    StorageType = locationItem.StorageType,
                    IsFromMobile = true,                   
                    ScannedDateInt =locationItem.ScannedDateTime.DateToInt(),
                    CreatedDate = System.DateTime.Now,                    
                    CreatedUserName = locationItem.ScannedUserName,
                    LuDate = System.DateTime.Now,
                    LuUserId =0,
                    CustomerId = Convert.ToInt32(customerCode)
                };

                _context.LocationItems.Add(item);

                //-----Check for latest uploads exist in carton location table

                var countLatestScans = _context.LocationItems.Where(x => x.ScanDateTime >= locationItem.ScannedDateTime 
                                                                    && x.BarCode.Trim() == locationItem.BarCode.Trim()).Count();

                if (countLatestScans == 0)
                {

                    if (itemStorage != null)
                    {
                        itemStorage.LocationCode = locationItem.LocationCode;
                        itemStorage.LastScannedDateTime = locationItem.ScannedDateTime;
                        itemStorage.LastScannedUserName = locationItem.ScannedUserName;
                        itemStorage.LastUpdateDate = System.DateTime.Now.DateToInt();
                        _context.Entry(itemStorage).State = EntityState.Modified;
                    }
                }
                //------------------------------------------------------------------------              

                _context.SaveChanges();
            }

            return true;


        }
    }
}
