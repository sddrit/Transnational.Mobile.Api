using MediatR;
using TransnationalLanka.Rms.Mobile.Services.RequestDetail.Core.Request;

namespace TransnationalLanka.Rms.Mobile.Services.RequestDetail.Core.Handler
{
    public class GetRequestByCartonRequestHandler : IRequestHandler<GetRequestByCartonRequest, Dal.Entities.RequestDetail>
    {
        private readonly IRequestDetailService _requestDetailService;

        public GetRequestByCartonRequestHandler(IRequestDetailService requestDetailService)
        {
            _requestDetailService = requestDetailService;
        }      
        
        public async Task<Dal.Entities.RequestDetail> Handle(GetRequestByCartonRequest request, CancellationToken cancellationToken)
        {
            return await _requestDetailService.GetCartonByRequestNo(request.RequestNo, request.CartonNo);
        }
    }
}
