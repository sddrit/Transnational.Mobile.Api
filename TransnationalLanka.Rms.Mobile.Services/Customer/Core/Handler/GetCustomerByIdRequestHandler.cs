using MediatR;
using TransnationalLanka.Rms.Mobile.Services.Customer.Core.Request;

namespace TransnationalLanka.Rms.Mobile.Services.Customer.Core.Handler
{
    public class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, Dal.Entities.Customer>
    {
        private readonly ICustomerService _customerService;

        public GetCustomerByIdRequestHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<Dal.Entities.Customer> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            return await _customerService.GetCustomerById(request.CustomerCode);
        }
    }
}
