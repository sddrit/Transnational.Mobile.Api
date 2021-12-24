using GuardNet;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Core.Extensions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Dal.Entities;
using TransnationalLanka.Rms.Mobile.Services.Customer.Core.Request;
using TransnationalLanka.Rms.Mobile.Services.Item.Core.Request;
using TransnationalLanka.Rms.Mobile.Services.Location.Core;

namespace TransnationalLanka.Rms.Mobile.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly RmsDbContext _context;
        private readonly IMediator _mediator;

        public LocationService(RmsDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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

        public async Task<List<AddLocationResult>> AddLocationItem(List<LocationItemDto> locationItems)
        {
            var result = new List<AddLocationResult>();

            foreach (var locationItem in locationItems)
            {
                try
                {
                    Guard.NotNullOrEmpty("BarCode", nameof(locationItem.BarCode));
                    Guard.NotNullOrEmpty("Location Code", nameof(locationItem.LocationCode));
                    Guard.NotNullOrEmpty("Scanned DateTime", nameof(locationItem.ScannedDateTime));

                    var cartonNo = int.Parse(locationItem.BarCode);
                    var customerCode = "0";
                    ItemStorage itemStorage = null;

                    try
                    {
                        itemStorage = await _mediator.Send(new GetItemByBarCodeRequest()
                        {
                            CartonNo = cartonNo
                        });
                    }
                    catch (ServiceException)
                    {
                        //If carton is not exists in inventory
                        cartonNo = 0;
                    }

                    if (itemStorage != null)
                    {
                        var customer = await _mediator.Send(new GetCustomerByIdRequest()
                        {
                            CustomerCode = itemStorage.CustomerId
                        });

                        customerCode = customer.CustomerCode;
                    }

                    var item = new LocationItem()
                    {
                        CartonNo = cartonNo,
                        BarCode = locationItem.BarCode,
                        LocationCode = locationItem.LocationCode,
                        ScanDateTime = locationItem.ScannedDateTime,
                        StorageType = locationItem.StorageType,
                        IsFromMobile = true,
                        ScannedDateInt = locationItem.ScannedDateTime.DateToInt(),
                        CreatedDate = System.DateTime.Now,
                        CreatedUserName = locationItem.ScannedUserName,
                        LuDate = System.DateTime.Now,
                        LuUserId = 0,
                        CustomerId = Convert.ToInt32(customerCode)
                    };

                    var isLatestRecord = await _context.LocationItems.AllAsync(x =>
                        x.ScanDateTime < locationItem.ScannedDateTime
                        && x.BarCode.Trim() == locationItem.BarCode.Trim());

                    if (isLatestRecord && itemStorage != null)
                    {
                        itemStorage.LocationCode = locationItem.LocationCode;
                        itemStorage.LastScannedDateTime = locationItem.ScannedDateTime;
                        itemStorage.LastScannedUserName = locationItem.ScannedUserName;
                        itemStorage.LastUpdateDate = System.DateTime.Now.DateToInt();
                        _context.Entry(itemStorage).State = EntityState.Modified;
                    }

                    _context.LocationItems.Add(item);

                    await _context.SaveChangesAsync();

                    result.Add(new AddLocationResult()
                    {
                        Barcode = locationItem.BarCode,
                        Success = true
                    });
                }
                catch (Exception e)
                {
                    result.Add(new AddLocationResult()
                    {
                        Barcode = locationItem.BarCode,
                        Success = false,
                        Error = e.Message
                    });
                }
            }
            return result;
        }
    }
}
