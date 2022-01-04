using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;

namespace TransnationalLanka.Rms.Mobile.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly RmsDbContext _context;

        public CustomerService(RmsDbContext context)
        {
            _context = context;
        }

        public async Task<Dal.Entities.Customer> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.TrackingId == id);

            if (customer == null)
            {               
               throw new ServiceException(new ErrorMessage[]
               {
                     new ErrorMessage()
                     {
                         Code = string.Empty,
                         Message =  $"Unable to find customer id {id}"
                     }
               });
            }

            return customer;
        }
    }
}
