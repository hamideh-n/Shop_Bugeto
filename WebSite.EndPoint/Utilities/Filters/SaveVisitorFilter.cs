using Application.Visitors.SaveVisitorInfo;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using UAParser;

namespace WebSite.EndPoint.Utilities.Filters
{
    // با استفاده از اکشن فیلنر که ما روی کنترلرها قرار می دهیم  ممکن یه اکشن داشته باشیم که با استفاده از ایجکس می تواند چندین بار برای هر رکویست
    //استفاده می شود و چندین بار اطلاعات بازدید کننده ذخیره می شود 
    // ابیجکس هارو اگر نمی خواهیم کانترش بیوفته اطلاعات برای ما  و ذخیره شود ا    
    public class SaveVisitorFilter : IActionFilter
    {
        private readonly ISaveVisitorInfoService _saveVisitorInfoService;
        public SaveVisitorFilter(ISaveVisitorInfoService saveVisitorInfoService)
        {
            _saveVisitorInfoService= saveVisitorInfoService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var controllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            var userAgent = context.HttpContext.Request.Headers["User_Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo clientInfo=uaParser.Parse(userAgent);
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            var currentUrl = context.HttpContext.Request.Path;
            var request=context.HttpContext.Request;

            _saveVisitorInfoService.Execute(new RequestSaveVisitorInfoDto
            {
                Browser = new VisitorVersionDto
                {
                    Family = clientInfo.UA.Family,
                    Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
                },
                CurrentLink = currentUrl,
                Device = new DeviceDto
                {
                    Brand = clientInfo.Device.Brand,
                    Family = clientInfo.Device.Family,
                    IsSpider = clientInfo.Device.IsSpider,
                    Model = clientInfo.Device.Model,
                },
                    Ip = ip,
                    Method=request.Method,
                    OperationSystem = new VisitorVersionDto {
                    Family =clientInfo.Device.Family,
                    Version= $"{clientInfo.OS.Major}.{clientInfo.OS.Minor}.{clientInfo.OS.Patch}",
                    },
                    PhisicalPath=$"{controllerName}/{actionName}",
                    Protocol=request.Protocol,
                    ReferrerLink=referer,

            }) ;



        }
    }
}
