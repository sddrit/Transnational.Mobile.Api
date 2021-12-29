using MediatR;
using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Extensions;
using TransnationalLanka.Rms.Mobile.Dal;
using TransnationalLanka.Rms.Mobile.Services.PickList.Core;
using TransnationalLanka.Rms.Mobile.Services.RequestDetail.Core.Request;

namespace TransnationalLanka.Rms.Mobile.Services.PickList
{
    public class PickListService : IPickListService
    {
        private readonly RmsDbContext _context;
        private readonly IMediator _mediator;

        public PickListService(RmsDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public List<PickListDto> GetPickLists(string deviceId)
        {
            var pickList = _context.PickLists.Where(p => p.LastSentDeviceId.ToLower() == deviceId.ToLower() && p.IsPicked == false && p.Deleted == false).
                  Select(p => new PickListDto()
                  {
                      AssignedUserId = p.AssignedUserId,
                      Barcode = p.Barcode,
                      CartonNo = p.CartonNo,
                      LocationCode = p.LocationCode,
                      PickListNo = p.PickListNo,
                      RequestNo = p.RequestNo,
                      Status = p.Status,
                      TrackingId = p.TrackingId,
                      WareHouseCode = p.WareHouseCode
                  }).ToList();

            return pickList;

        }

        public async Task<bool> UpdatePickStatus(List<PickListInsertDto> pickListItems)
        {
            var result = new List<AddPickListResult>();

            foreach (var pickListItem in pickListItems)
            {
                var pickList = _context.PickLists.Where(x => x.PickListNo == pickListItem.PickListNo && x.CartonNo == pickListItem.CartonNo && x.IsPicked == false ).FirstOrDefault();

                try
                {

                    if (pickList != null)
                    {
                        pickList.PickDate = pickListItem.PickDateTime.DateToInt();
                        pickList.IsPicked = true;
                        pickList.LastSentDeviceId = "Uploaded";
                        pickList.PickedUserId = pickListItem.PickedUserId;
                        pickList.LuUserId = pickListItem.PickedUserId;
                        pickList.LuDate = System.DateTime.Now;
                        _context.Entry(pickList).State = EntityState.Modified;


                        var requestDetails = await _mediator.Send(new GetRequestByCartonRequest()
                        {
                            CartonNo = pickListItem.CartonNo,
                            RequestNo = pickList.RequestNo
                        });

                        if (requestDetails != null)
                        {
                            requestDetails.Picked = true;
                            requestDetails.FromMobile = true;
                            requestDetails.PickListNo = pickListItem.PickListNo;
                        }

                        var itemStorage = _context.ItemStorages.Where(x => x.CartonNo == pickListItem.CartonNo).First();

                        if (itemStorage.CartonNo > 0 && (itemStorage.LastScannedDateTime == null || itemStorage.LastScannedDateTime <= pickListItem.PickDateTime))
                        {

                            itemStorage.LocationCode = "000000PICK";
                        }

                        _context.SaveChanges();

                        result.Add(new AddPickListResult()
                        {
                            CartonNo = pickListItem.CartonNo,
                            PickListNo = pickList.RequestNo,
                            Success = true
                        });
                    }
                }
                catch (Exception e)
                {
                    result.Add(new AddPickListResult()
                    {
                        CartonNo = pickListItem.CartonNo,
                        PickListNo = pickList.RequestNo,
                        Success = false,
                        Error = e.Message
                    });

                }
            }

            return true;

        }

        public async Task<bool> MarkAsDeletedFromDevice(List<PickListMarkDeleteDto> pickListMarkDeleteDtos)
        {

            var result = new List<MarkDeletedFromDeviceResult>();

            foreach (var pickListMarkDeleteDtoItem in pickListMarkDeleteDtos)
            {

                var pickList = await _context.PickLists.Where(p => p.TrackingId == pickListMarkDeleteDtoItem.TrackingId).FirstOrDefaultAsync();
                try
                {
                    if (pickList != null)
                    {
                        pickList.Deleted = true;
                        _context.Entry(pickList).State = EntityState.Modified;

                        _context.SaveChanges();

                        result.Add(new MarkDeletedFromDeviceResult()
                        {
                            TrackingId = pickListMarkDeleteDtoItem.TrackingId,
                            Success = true
                        });
                    }
                }
                catch (Exception ex)
                {

                    result.Add(new MarkDeletedFromDeviceResult()
                    {
                        TrackingId = pickListMarkDeleteDtoItem.TrackingId,
                        Success = false,
                        Error = ex.Message
                    });
                }
            }
            return true;

        }

    }
}
