using System.ComponentModel.DataAnnotations;

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
    }
}
