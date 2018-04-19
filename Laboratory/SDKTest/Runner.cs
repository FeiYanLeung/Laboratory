using Laboratory.Core;
using System;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Laboratory.SDKTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "SDK"; }
        }

        public void Run()
        {
            #region Send SMS

            ITopClient client = new DefaultTopClient("https://eco.taobao.com/router/rest", AppConfig.ALI_DY_KEY, AppConfig.ALI_DY_SECRET, "json");
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = "";
            req.SmsType = "normal";
            req.SmsFreeSignName = "大鱼测试";
            req.SmsParam = "{\"customer\":\"Test\"}";
            req.RecNum = AppConfig.PHONE;
            req.SmsTemplateCode = "SMS_6811764";
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            Console.Write(rsp.Body);

            #endregion
        }
    }
}
