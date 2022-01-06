using TransnationalLanka.Rms.Mobile.Dal.Helper;
using TransnationalLanka.Rms.Mobile.Services.Location.Core;

namespace TransnationalLanka.Rms.Mobile.Services.Location;

public interface ILocationService
{
    Task<LocationDto> GetLocationByCode(string code);
    Task<List<AddLocationResult>> AddLocationItem(List<LocationItemDto> locationItems);
    List<LocationItemDetailDto> GetScanBySummary(string userName);
    Task<PagedResponse<LocationItemViewDto>> GetScanByDetail(string userName, DateTime dtUtc, string searchText = null, int pageIndex = 1, int pageSize = 10);
}