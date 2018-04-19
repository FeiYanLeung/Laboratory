using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Laboratory.SinaShortUrlTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "短网址生成"; }
        }

        public void Run()
        {
            #region 短网址获取
            //短网址
            string strTargetUrl = "http://www.leung.com";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string api_url = "http://api.t.sina.com.cn/short_url/shorten.json";
                    string api_parms = string.Format("source={0}&url_long={1}", 1681459862, HttpUtility.UrlEncode(strTargetUrl));
                    //参数0为sina微博默认appkey
                    string response = client.DownloadString(string.Join("?", api_url, api_parms));

                    if (!string.IsNullOrEmpty(response))
                    {
                        List<UrlShortModel> lstModels = JsonConvert.DeserializeObject<List<UrlShortModel>>(response);
                        lstModels.ForEach(e =>
                        {
                            Console.WriteLine("UrlLong:{0}；UrlShort:{1}；Type:{2}", e.UrlLong, e.UrlShort, e.Type);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion
        }
    }
}
