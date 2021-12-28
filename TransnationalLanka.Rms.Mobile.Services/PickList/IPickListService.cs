﻿using TransnationalLanka.Rms.Mobile.Services.PickList.Core;

namespace TransnationalLanka.Rms.Mobile.Services.PickList
{
    public  interface IPickListService
    {
        public  List<Dal.Entities.PickList> GetPickLists(string deviceId);

        public Task<bool> UpdatePickStatus(List<PickListDto> pickListItems);
    }
}
