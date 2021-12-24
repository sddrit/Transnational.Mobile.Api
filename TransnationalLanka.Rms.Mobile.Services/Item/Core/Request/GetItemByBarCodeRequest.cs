using MediatR;

namespace TransnationalLanka.Rms.Mobile.Services.Item.Core.Request
{
    public class GetItemByBarCodeRequest : IRequest<Dal.Entities.ItemStorage>
    {
        public int CartonNo { get; set; }
    }
}
