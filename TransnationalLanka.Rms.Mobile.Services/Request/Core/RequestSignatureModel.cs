using Microsoft.AspNetCore.Http;

namespace TransnationalLanka.Rms.Mobile.Services.Request.Core
{
    public  class RequestSignatureModel
    {
        public string RequestNo { get; set; }
        public string UserName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
