namespace TransnationalLanka.Rms.Mobile.Services.PickList.Core
{
    public  class PickListInsertDto
    {
        public string PickListNo { get; set; }       
        public int CartonNo { get; set; }
        public DateTime PickDateTime { get; set; }
        public int PickedUserId { get; set; }
    }

    public class PickListDto
    {
        public long TrackingId { get; set; }      
        public string PickListNo { get; set; }       
        public int CartonNo { get; set; }       
        public string Barcode { get; set; }      
        public string LocationCode { get; set; }        
        public string WareHouseCode { get; set; }
        public int? AssignedUserId { get; set; }
        public int Status { get; set; }
        public string RequestNo { get; set; }

    }

    public class PickListMarkDeleteDto
    {
        public string PickListNo { get; set; }
    }
}
