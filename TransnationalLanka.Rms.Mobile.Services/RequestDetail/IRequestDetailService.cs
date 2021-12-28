using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransnationalLanka.Rms.Mobile.Services.RequestDetail
{
    public  interface IRequestDetailService
    {
        Task<Dal.Entities.RequestDetail> GetCartonByRequestNo(string requestNo, int cartonNo);
    }
}
