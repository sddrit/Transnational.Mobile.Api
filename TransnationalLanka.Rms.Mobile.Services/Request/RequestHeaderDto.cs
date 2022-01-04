namespace TransnationalLanka.Rms.Mobile.Services.Request
{
    public class RequestHeaderDto
    {
        public string RequestNo { get; set; }
        public string RequestType { get; set; }
        public DateOnly RequestDate { get; set; }
        public string CutomerCode  { get; set; }
        public string CutomerName { get; set; }
        public List<RequestDetailDto> RequestDetailsDtos { get; set; }
    }
}
