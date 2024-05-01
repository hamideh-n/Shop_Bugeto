namespace Domain.Visitors
{
    public class Visitor
    {//ای پی یاردید کننده 
        public string Ip { get; set; }
        //لینک که کاربر در اون قرار دارد
        public string CurrentLink { get; set; }
        //لینک کاربر از اون امده
        public string ReferrerLink { get; set; }
        //متودکاربر از اون لینک بازدید کرده رو
        public string Method { get; set; }
        //http یا https
        public string Protocol { get; set; }
        //کدام controller و action 
        public string PhisicalPath { get; set; }
        //اطلاعات...رانمایش میدهد
        public VisitorVersion Browser  { get; set; }
        //اطلاعات سیسنم عامل
        public VisitorVersion OperationSystem { get; set; }
        public Device Device { get; set; }
    }
}
