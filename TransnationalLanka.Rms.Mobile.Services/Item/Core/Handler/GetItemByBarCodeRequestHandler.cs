using MediatR;
using TransnationalLanka.Rms.Mobile.Dal.Entities;
using TransnationalLanka.Rms.Mobile.Services.Item.Core.Request;

namespace TransnationalLanka.Rms.Mobile.Services.Item.Core.Handler
{
    public class GetItemByBarCodeRequestHandler : IRequestHandler<GetItemByBarCodeRequest, ItemStorage>
    {
        private readonly IItemService _itemService;

        public GetItemByBarCodeRequestHandler(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<ItemStorage> Handle(GetItemByBarCodeRequest request, CancellationToken cancellationToken)
        {
            return await _itemService.GetItemByBarCode(request.CartonNo);
        }
    }
}
