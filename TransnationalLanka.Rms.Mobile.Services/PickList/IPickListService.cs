using TransnationalLanka.Rms.Mobile.Services.PickList.Core;

namespace TransnationalLanka.Rms.Mobile.Services.PickList
{
    public  interface IPickListService
    {
        public  List<PickListDto> GetPickLists(string deviceId);

        public Task<bool> UpdatePickStatus(List<PickListInsertDto> pickListItems);

        public Task<bool> MarkAsDeletedFromDevice(PickListMarkDeleteDto pickListMarkDeleteDtos);
    }
}
