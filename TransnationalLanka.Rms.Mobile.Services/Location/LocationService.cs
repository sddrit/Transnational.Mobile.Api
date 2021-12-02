using GuardNet;
using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;
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
                Name = location.Name
            };
        }
    }
}
