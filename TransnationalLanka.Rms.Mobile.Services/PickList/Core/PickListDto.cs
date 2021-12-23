namespace TransnationalLanka.Rms.Mobile.Services.PickList.Core
{
    public  class PickListDto
    {
        public string PickListNo { get; set; }       
        public int CartonNo { get; set; }
        public DateTime PickDateTime { get; set; }
        public int PickedUserId { get; set; }
    }
}
