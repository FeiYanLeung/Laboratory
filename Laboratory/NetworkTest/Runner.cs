using Laboratory.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratory.NetworkTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "网络"; }
        }

        public void Run()
        {
            var webRequest = WebRequest.Create("https://soke.ga/search") as HttpWebRequest;
            var resp = webRequest.Post(new KeyValuePair<string, string>("q", "123"));
            Console.WriteLine(resp);

            return;
            for (int i = 0; i < 10; i++)
            {
                Parallel.For(0, 100, (item) =>
                  {
                      var res = Network.Request.PostAsync("http://local.59bang.com/user/login", new KeyValuePair<string, string>[] {
                          new KeyValuePair<string, string>("UserName","10013"),
                          new KeyValuePair<string, string>("Password","123456"),
                          new KeyValuePair<string, string>("RememberMe","false")
                      });

                      Debug.Print(item.ToString());
                  });
            }

            Debug.Flush();

            return;

            var statutory_holidays = this.StatutoryHolidays();
            var statutory_holidays2 = this.StatutoryHolidays2();


            if (statutory_holidays != null && statutory_holidays2 != null)
            {

            }
        }


        public virtual List<StatutoryHoliday> StatutoryHolidays(int? year = null, params int[] month)
        {
            var statutory_holidays = new List<StatutoryHoliday>();

            #region 请求范围,默认请求当前年份所有月的假日信息

            var months = new List<string>();

            if (year.HasValue && month.Length > 0)
            {
                months = month.Where(w => w > 0 && w < 13)
                    .Select(q => String.Concat(year.Value, q.ToString().PadLeft(2, '0')))
                    .ToList();
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    months.Add(String.Concat(DateTime.Now.Year, i.ToString().PadLeft(2, '0')));
                }
            }

            #endregion

            #region 请求三方假日安排接口并解析数据

            var srcResponse = Network.Request.Get(String.Concat("http://www.easybots.cn", $"/api/holiday.php?ak=k438.4928942901b4d60a898b165e17565976@{AppConfig.DOMAIN_SUFFIX}&m=", String.Join(",", months)));

            //按月份划分数据
            var regex_holidays = new Regex("(\"\\d{6}\":\\{(\"\\d{2}\":\"[0|1|2]\"[,|\\}])+)");
            //匹配月份数值
            var regex_holiday_month = new Regex("\\d{6}");
            //匹配月份值中的日期信息
            var regex_holiday_month_day = new Regex("(\"\\d{2}\":\"[0|1|2]\")");

            //按月划分的假日安排
            var holidayCollection = regex_holidays.Matches(srcResponse);
            foreach (var holiday in holidayCollection)
            {
                var holidayMonth = regex_holiday_month.Match(holiday.ToString());
                var dayCollection = regex_holiday_month_day.Matches(holiday.ToString());

                var statutory_holiday = new StatutoryHoliday()
                {
                    Month = holidayMonth.Value
                };

                foreach (var day in dayCollection)
                {
                    var _day = day.ToString().Replace("\"", "").Split(':');
                    statutory_holiday.Days.Add(_day[0], int.Parse(_day[1]));
                }
                statutory_holidays.Add(statutory_holiday);
            }

            #endregion

            return statutory_holidays;
        }

        public virtual List<StatutoryHoliday> StatutoryHolidays2(int? year = null, params int[] month)
        {
            var statutory_holidays = new List<StatutoryHoliday>();

            #region 请求范围,默认请求当前年份所有月的假日信息

            var months = new List<string>();

            if (year.HasValue && month.Length > 0)
            {
                months = month.Where(w => w > 0 && w < 13)
                    .Select(q => String.Concat(year.Value, q.ToString().PadLeft(2, '0')))
                    .ToList();
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    months.Add(String.Concat(DateTime.Now.Year, i.ToString().PadLeft(2, '0')));
                }
            }

            #endregion

            #region 请求三方假日安排接口并解析数据

            var srcResponse = Network.Request.Get(String.Concat("http://tool.bitefu.net", "/jiari/?type=list&apikey=123456&d=", String.Join(",", months)));

            //按月份划分数据
            var regex_holidays = new Regex("(\"\\d{6}\":\\{(\"\\d{4}\":\"[0|1|2]\"[,|\\}])+)");
            //匹配月份数值
            var regex_holiday_month = new Regex("\\d{6}");
            //匹配月份值中的日期信息
            var regex_holiday_month_day = new Regex("(\"\\d{4}\":\"[0|1|2]\")");

            //按月划分的假日安排
            var holidayCollection = regex_holidays.Matches(srcResponse);
            foreach (var holiday in holidayCollection)
            {
                var holidayMonth = regex_holiday_month.Match(holiday.ToString());
                var dayCollection = regex_holiday_month_day.Matches(holiday.ToString());

                var statutory_holiday = new StatutoryHoliday()
                {
                    Month = holidayMonth.Value
                };

                foreach (var day in dayCollection)
                {
                    var _day = day.ToString().Replace("\"", "").Split(':');
                    statutory_holiday.Days.Add(_day[0].Substring(2), int.Parse(_day[1]));
                }
                statutory_holidays.Add(statutory_holiday);
            }

            #endregion

            return statutory_holidays;
        }

        private void phoneNumberLocation()
        {
            var parameters = new KeyValuePair<string, string>[]{
                new KeyValuePair<string,string>("m","13800138000"),
                new KeyValuePair<string,string>("output","json"),
                new KeyValuePair<string,string>("callback","resolver")
            };

            var srcLocation = Network.Request.Get("http://v.showji.com/Locating/showji.com2016234999234.aspx", parameters);

            var regexJson = new Regex(@"\{(""\w+\S+"")+\}");
            if (regexJson.IsMatch(srcLocation))
            {
                var dstLocation = regexJson.Match(srcLocation).Value;

                var location = JsonConvert.DeserializeObject<PhoneNumberAttribution>(dstLocation);
                if (location != null)
                {

                }

                return;
            }
            Console.WriteLine("没有匹配");
        }

        [Serializable]
        public class PhoneNumberAttribution
        {
            public string Mobile { get; set; }
            public bool QueryResult { get; set; }
            public string TO { get; set; }
            public string Corp { get; set; }
            public string Province { get; set; }
            public string City { get; set; }
            public string AreaCode { get; set; }
            public string PostCode { get; set; }
            public string VNO { get; set; }
            public string Card { get; set; }
        }

        private void download()
        {
            string download_url = $"https://files.{AppConfig.DOMAIN_SUFFIX}/@/test/17101/11337/file/2017010620170106181207_6464.jpg";

            for (int i = 1; i <= 20; i++)
            {
                using (var client2 = new WebClient())
                {
                    Console.WriteLine("第{0}次下载", i);
                    var filename = string.Format(@"D:\download\{0}.jpg", i);
                    FileInfo info = new FileInfo(filename);
                    if (info.Exists) info.Delete();
                    client2.DownloadFile(download_url, filename);
                    Thread.Sleep(500);
                }
            }
        }

    }
}
