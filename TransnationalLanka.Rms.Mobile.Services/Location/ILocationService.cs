using TransnationalLanka.Rms.Mobile.Services.Location.Core;

namespace TransnationalLanka.Rms.Mobile.Services.Location;

public interface ILocationService
{
    Task<LocationDto> GetLocationByCode(string code);

    bool AddLocationItem(List<LocationItemDto> locationItems);
}