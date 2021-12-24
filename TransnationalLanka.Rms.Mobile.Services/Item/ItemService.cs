using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;

namespace TransnationalLanka.Rms.Mobile.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly RmsDbContext _context;

        public ItemService(RmsDbContext context)
        {
            _context = context;
        }

        public async Task<Dal.Entities.ItemStorage> GetItemByBarCode(int cartonNo)
        {
            var item = await _context.ItemStorages.FirstOrDefaultAsync(i => i.CartonNo == cartonNo);

            if (item == null)
            {
                throw new ServiceException(null, $"Unable to find carton no {cartonNo}");
            }

            return item;
        }
    }
}
