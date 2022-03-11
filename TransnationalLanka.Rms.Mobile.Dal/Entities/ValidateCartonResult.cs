namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{
    public class ValidateCartonResult
    {
        public int? CartonNo { get; set; }
        public string? Reason { get; set; }
        public bool? CanProcess { get; set; }
        public string? CartonStatus { get; set; }

    }

    public class CartonValidationModel
    {
        public int CartonNo { get; set; }
        public int ToCartonNo { get; set; }
    }

    public class CartonSplitResultModel
    {
        public int OutValue { get; set; }
       
    }

}
