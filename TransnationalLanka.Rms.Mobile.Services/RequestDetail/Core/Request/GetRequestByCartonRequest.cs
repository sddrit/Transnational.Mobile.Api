using MediatR;

namespace TransnationalLanka.Rms.Mobile.Services.RequestDetail.Core.Request
{
    public class GetRequestByCartonRequest : IRequest<Dal.Entities.RequestDetail>
    {
        public int CartonNo { get; set; }
        public string RequestNo { get; set; }
    }
}
