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
}
