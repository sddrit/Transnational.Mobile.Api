using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace TransnationalLanka.Rms.Mobile.WebApi.Models
{
    public class CreateDocketBindingModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RequestNumber { get; set; }
    }

    public class SignatureRequestBindingModel
    {
        [Required]
        public string RequestNo { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public long DocketSerialNo { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerNIC { get; set; }
        public string? CustomerDesignation { get; set; }
        public string? CustomerDepartment { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public bool? IsSplitRequest { get; set; }
    }
}
