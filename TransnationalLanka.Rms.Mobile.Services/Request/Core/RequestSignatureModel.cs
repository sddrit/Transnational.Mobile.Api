using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace TransnationalLanka.Rms.Mobile.Services.Request.Core
{
    public class RequestSignatureModel
    {
        [Required]
        public string RequestNo { get; set; }
        
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FileName { get; set; }

        public string ContentType { get; set; }
        
        [Required]
        public long DocketSerialNo { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerNIC { get; set; }

        public string CustomerDesignation { get; set; }
        public string CustomerDepartment { get; set; }

        public double? Lat { get; set; }
        public double? Lon { get; set; }


    }
}
