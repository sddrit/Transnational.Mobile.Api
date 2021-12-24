namespace TransnationalLanka.Rms.Mobile.Services.Customer;

public interface ICustomerService
{
    Task<Dal.Entities.Customer> GetCustomerById(int id);
}