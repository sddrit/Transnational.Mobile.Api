using Microsoft.EntityFrameworkCore;
using TransnationalLanka.Rms.Mobile.Core.Exceptions;
using TransnationalLanka.Rms.Mobile.Dal;

namespace TransnationalLanka.Rms.Mobile.Services.RequestDetail
{
    public class RequestDetailService:IRequestDetailService
    {
        private readonly RmsDbContext _context;

        public RequestDetailService(RmsDbContext context)
        {
            _context = context;
        }

        public async Task<Dal.Entities.RequestDetail> GetCartonByRequestNo(string requestNo,int cartonNo)
        {

            var requestDetail = await _context.RequestDetails.FirstOrDefaultAsync(x => x.RequestNo == requestNo &&  x.CartonNo == cartonNo  );

            if (requestDetail == null)
            {
                throw new ServiceException(null, $"Unable to find request detail  {cartonNo}");
            }
            
            return requestDetail;
        }
    }
}
