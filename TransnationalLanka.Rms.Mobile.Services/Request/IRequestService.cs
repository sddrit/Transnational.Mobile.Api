using TransnationalLanka.Rms.Mobile.Dal.Entities;
using TransnationalLanka.Rms.Mobile.Dal.Helper;
using TransnationalLanka.Rms.Mobile.Services.Request.Core;

namespace TransnationalLanka.Rms.Mobile.Services.Request
{
    public  interface IRequestService
    {
        Task<Dal.Entities.RequestDetail> GetCartonByRequestNo(string requestNo, int cartonNo);
        Task<DocketDto> GetDocketDetails(string requestNo, string userName);
        Task<PagedResponse<SearchRequestResult>> SearchRequestHeader(string requestNo, string customerName,int pageIndex, int pageSize);
        Task<List<ValidateCartonResult>> ValidateRequest(string requestNo, int cartonNo);
        Task<bool> UploadSignature(RequestSignatureModel model);
    }
}
