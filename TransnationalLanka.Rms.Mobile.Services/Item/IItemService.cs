namespace TransnationalLanka.Rms.Mobile.Services.Item;

public interface IItemService
{
    Task<Dal.Entities.ItemStorage> GetItemByBarCode(int cartonNo);
}