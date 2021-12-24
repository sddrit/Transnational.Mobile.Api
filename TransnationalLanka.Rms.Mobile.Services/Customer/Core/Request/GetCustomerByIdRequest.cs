using MediatR;

namespace TransnationalLanka.Rms.Mobile.Services.Customer.Core.Request
{
    public class GetCustomerByIdRequest : IRequest<Dal.Entities.Customer>
    {
        public int CustomerCode { get; set; }
    }
}
