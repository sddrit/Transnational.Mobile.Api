using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Core.Extensions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Services.PickList.Core;

namespace TransnationalLanka.Rms.Mobile.Services.PickList
{
    public class PickListService : IPickListService
    {
        private readonly RmsDbContext _context;

        public PickListService(RmsDbContext context)
        {
            _context = context;
        }

        public List<Dal.Entities.PickList> GetPickLists(string deviceId)
        {
            var pickLists = _context.PickLists.Where(p => p.LastSentDeviceId.ToLower() == deviceId.ToLower()).ToList();

            if (pickLists == null)
            {

                throw new ServiceException(string.Empty, $"Unable to find pick lists");
            }

            return pickLists;
        }

        public bool UpdatePickStatus(List<PickListDto> pickListItems)
        {

            foreach (var pickListItem in pickListItems)
            {

                var pickList = _context.PickLists.Where(x => x.PickListNo == pickListItem.PickListNo && x.CartonNo == pickListItem.CartonNo).FirstOrDefault();

                if (pickList != null)
                {
                    pickList.PickDate = pickListItem.PickDateTime.DateToInt();
                    pickList.IsPicked = true;
                    pickList.LastSentDeviceId = "Uploaded";
                    pickList.PickedUserId = pickListItem.PickedUserId;
                    pickList.LuUserId = pickListItem.PickedUserId;
                    pickList.LuDate = System.DateTime.Now;
                    _context.Entry(pickList).State = EntityState.Modified;
                }
                _context.SaveChanges();

                var requestDetails = _context.RequestDetails.Where(x => x.RequestNo == pickList.RequestNo && x.CartonNo == pickListItem.CartonNo).FirstOrDefault();

                if (requestDetails != null)
                {
                    requestDetails.Picked = true;
                    requestDetails.FromMobile = true;
                    requestDetails.PickListNo = pickListItem.PickListNo;

                }
                _context.SaveChanges();

                var itemStorage = _context.ItemStorages.Where(x=>x.CartonNo == pickListItem.CartonNo).First();

                if (itemStorage.CartonNo>0 && (itemStorage.LastScannedDateTime ==null || itemStorage.LastScannedDateTime<= pickListItem.PickDateTime))
                {

                    itemStorage.LocationCode = "000000PICK";
                }

                _context.SaveChanges();
            }

            return true;


        }

    }
}
