using Application.Interface.DbContext;
using Domain.Visitors;
using MongoDB.Driver;

namespace Application.Visitors.SaveVisitorInfo
{
    public interface ISaveVisitorInfoService
    {
        void Execute(RequestSaveVisitorInfoDto request);
    }
    public class SaveVisitorInfoService : ISaveVisitorInfoService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _visitorMongoCollection;
        public SaveVisitorInfoService(IMongoDbContext<Visitor> mongoDbContext, IMongoCollection<Visitor> visitorMongoCollection)
        {
            _mongoDbContext=mongoDbContext;
            _visitorMongoCollection= _mongoDbContext.GetCollection();
        }
        public void Execute(RequestSaveVisitorInfoDto request)
        {
            _visitorMongoCollection.InsertOne(new Visitor 
            {
            Browser=new VisitorVersion
            {
                Family=request.Browser.Family,
                Version=request.Browser.Version
            },
            CurrentLink=request.CurrentLink,
            Device=new Device
            {
                   Brand=request.Device.Brand,
            },
            Ip=request.Ip,
            Method=request.Method,
            OperationSystem=new VisitorVersion
            {
                Family=request.OperationSystem.Family,
                Version=request.OperationSystem.Version
            }
            }
            );
        }
    }
    public class RequestSaveVisitorInfoDto
    {
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
        public VisitorVersionDto Browser { get; set; }
        //اطلاعات سیسنم عامل
        public VisitorVersionDto OperationSystem { get; set; }
        public DeviceDto Device { get; set; }
    }
    public class VisitorVersionDto
    {
        public string Version { get; set; }
        public string Family { get; set; }
    }
    public class DeviceDto
    {
        public string Brand { get; set; }
        public string Family { get; set; }
        public string Model { get; set; }
        //ایا باردید کننده ربات بوده یا واقعی
        public bool IsSpider { get; set; }
    }
}
