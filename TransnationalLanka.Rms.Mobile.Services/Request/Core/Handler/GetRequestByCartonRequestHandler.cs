using MediatR;
using TransnationalLanka.Rms.Mobile.Services.Request.Core.Request;


namespace TransnationalLanka.Rms.Mobile.Services.Request.Core.Handler
{
    public class GetRequestByCartonRequestHandler : IRequestHandler<GetRequestByCartonRequest, Dal.Entities.RequestDetail>
    {
        private readonly IRequestService _requestDetailService;

        public GetRequestByCartonRequestHandler(IRequestService requestDetailService)
        {
            _requestDetailService = requestDetailService;
        }      
        
        public async Task<Dal.Entities.RequestDetail> Handle(GetRequestByCartonRequest request, CancellationToken cancellationToken)
        {
            return await _requestDetailService.GetCartonByRequestNo(request.RequestNo, request.CartonNo);
        }
    }
}
