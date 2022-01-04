using TransnationalLanka.Rms.Mobile.Dal.Entities;

namespace TransnationalLanka.Rms.Mobile.Services.Request.Core
{
    public class DocketDto
    {
        public string RequestNo { get; set; }
        public string DocketType { get; set; }
        public int SerialNo { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PoNo { get; set; }
        public string ContactNo { get; set; }
        public string Department { get; set; }
        public bool IsPrintAlternativeNo { get; set; }
        public List<DocketEmptyDetail> EmptyDetails { get; set; }
        public List<DocketDetail> DocketDetails { get; set; }
        public string Route { get; set; }
    }
}
