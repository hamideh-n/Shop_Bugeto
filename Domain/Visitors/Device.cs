namespace Domain.Visitors
{
    public class Device
    {
        public string Brand { get; set; }
        public string Family { get; set; }
        public string Model { get; set; }
        //ایا باردید کننده ربات بوده یا واقعی
        public bool IsSpider { get; set; }
    }
}
