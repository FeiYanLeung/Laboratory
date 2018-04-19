using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory.RegexTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "正则表达式"; }
        }

        public void Run()
        {
            var areas = new string[] {
                "CRM.Web.Areas.Admin.Controllers.UserController",       //Admin
                "CRM.Web.Controllers.UserController"
            };

            var regexAreaName = new Regex(@"(?:Areas.)(\w+)", RegexOptions.IgnoreCase);
            foreach (var area in areas)
            {
                var matched_areas = regexAreaName.Match(area);
                if (matched_areas.Groups.Count > 1)
                {
                    Console.WriteLine(matched_areas.Groups[1]);
                    continue;
                }
                Console.WriteLine(area);
            }


            var jsonData = new List<string>(){
                "{\"begin\":{\"name\":\"开始\",\"val\":\"2018-03-08\"},\"end\":{\"name\":\"结束\",\"val\":\"2018-05-08\"},\"duration\":{\"name\":\"工作时长\",\"val\":\"61天\"}}",
                "[{\"id\":15,\"name\":\"数字0\",\"val\":\"5555\",\"type\":\"number\"},{\"id\":16,\"name\":\"金额0(元)\",\"val\":{\"val\":\"5555\",\"isCapital\":true},\"type\":\"price\"}]",
                "{\"name\":\"数字0\",\"val\":\"5555\"}",
                "{\"val\":\"5555\",\"isCapital\":true}",
                "123"
            };

            //匹配带有begin|end|duration的对象
            var regexObject = new Regex("\"(?:begin|end|duration)\"\\:{(\"(?:name|val)\"\\:\"(?:.+?)\"[,]?}?)+", RegexOptions.IgnoreCase);
            //匹配带有name|val的对象，val可以是一个值也可以是一个对象
            var regexObjectX = new Regex("\"(?:name|val)\"\\:(?:\\{\"val\":\"(.+?)\"|\"(.+?)\")");
            //匹配name|val
            var regexObjectValue = new Regex("\"(?:name|val)\"\\:\"(.+?)\"", RegexOptions.IgnoreCase);

            foreach (var data in jsonData)
            {
                var lineValues = new List<string>();
                if (regexObject.IsMatch(data))
                {
                    var chunks = regexObject.Matches(data);
                    foreach (var chunk in chunks)
                    {
                        var nameValueChunks = regexObjectValue.Matches(chunk.ToString());
                        foreach (var nameValueChunk in nameValueChunks)
                        {
                            lineValues.Add(regexObjectValue.Replace(nameValueChunk.ToString(), "$1"));
                        }
                    }
                }
                else if (regexObjectX.IsMatch(data))
                {
                    var chunks = regexObjectX.Matches(data);
                    foreach (var chunk in chunks)
                    {
                        var nameValueChunks = regexObjectValue.Matches(chunk.ToString());
                        foreach (var nameValueChunk in nameValueChunks)
                        {
                            lineValues.Add(regexObjectValue.Replace(nameValueChunk.ToString(), "$1"));
                        }
                    }
                }
                else
                {
                    lineValues.Add(data);
                }
                Console.WriteLine(string.Join(" ", lineValues));
            }

            return;
            //去除空格
            var regex_tw = new Regex(@"\s+");

            //匹配数字
            var regex_digit = new Regex(@"\d+");

            //id在右侧
            var regex_id_ir = new Regex(@"<=\S+", RegexOptions.IgnorePatternWhitespace);

            //id在左侧
            var regex_id_il = new Regex(@"[<|=]\S+", RegexOptions.IgnorePatternWhitespace);
            //替换formjson
            var regex_id_json = new Regex("\\\"id\\\":\\d+,", RegexOptions.IgnorePatternWhitespace);

            var values = ("1<1000,1000<=1 && 1<5000,5000<=1,1=一般,1=紧急（x）,1=非常紧急(x)").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var json = "{\"title\":\"物品采购\",\"abstact\":\"用于企业办公或所需材料的采购申请\",\"fields\":[{\"name\":\"物品类型\",\"type\":\"radio\",\"id\":0,\"isClick\":false,\"option\":{\"placeholder\":\"如：办公用品等\",\"isRequired\":true,\"optionItems\":[{\"text\":\"办公用品\"},{\"text\":\"生活用品\"},{\"text\":\"生产材料\"},{\"text\":\"其他\"}]},\"val\":\"\",\"isInGroup\":false},{\"name\":\"期望交付日期\",\"type\":\"date\",\"id\":1,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":true,\"isDateTime\":false},\"val\":\"\",\"isInGroup\":false},{\"name\":\"明细0\",\"type\":\"group\",\"children\":[{\"name\":\"名称\",\"type\":\"text\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"规格\",\"type\":\"text\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"数量\",\"type\":\"number\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"单位\",\"type\":\"text\",\"id\":7,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"金额\",\"type\":\"price\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}],\"isClick\":false,\"total\":[{\"name\":\"数量\",\"type\":\"number\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"金额\",\"type\":\"price\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}],\"option\":{\"btnText\":\"添加采购明细\"},\"id\":2,\"isInGroup\":false,\"clone\":[{\"name\":\"明细0\",\"items\":[{\"name\":\"名称\",\"type\":\"text\",\"id\":3,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"规格\",\"type\":\"text\",\"id\":4,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"数量\",\"type\":\"number\",\"id\":6,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"\",\"hasTotal\":true},\"val\":null,\"isInGroup\":true},{\"name\":\"单位\",\"type\":\"text\",\"id\":7,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true},\"val\":\"\",\"isInGroup\":true},{\"name\":\"金额\",\"type\":\"price\",\"id\":8,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":true,\"unit\":\"元\",\"isCapital\":true,\"hasTotal\":true,\"isShowThousands\":false},\"val\":null,\"isInGroup\":true}]}]},{\"name\":\"备注\",\"type\":\"paragraph\",\"id\":9,\"isClick\":false,\"option\":{\"placeholder\":\"请输入\",\"isRequired\":false},\"val\":\"\",\"isInGroup\":false},{\"name\":\"附件\",\"type\":\"file\",\"id\":10,\"isClick\":false,\"option\":{\"placeholder\":\"请选择\",\"isRequired\":false,\"isMulti\":true},\"val\":[],\"isInGroup\":false}],\"icon\":\"/content/img/approvalIcon/approvalIcon_9.png\",\"isNewVersion\":true,\"maxId\":11}";

            var json_values = regex_id_json.Matches(json);
            foreach (var json_value in json_values)
            {
                var _jv = json_value.ToString();
                var json_value_id = regex_digit.Match(_jv).Value;
                json = json.Replace(_jv, "\"id\":\"cid" + json_value_id + "\",");
            }

            var new_values = new List<string>();
            foreach (var value in values)
            {
                var new_value_chunks = new List<string>();

                var value_chunks = value.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var value_chunk in value_chunks)
                {
                    var _value_chunk = regex_tw.Replace(value_chunk, "");
                    if (_value_chunk.Contains("<="))
                    {
                        new_value_chunks.Add(regex_id_ir.Replace(_value_chunk, "<=$id"));
                    }
                    else
                    {
                        new_value_chunks.Add("$id" + regex_id_il.Match(_value_chunk).Value);
                    }
                }

                new_values.Add(string.Join(" && ", new_value_chunks));

            }

            Console.WriteLine(string.Join(",", new_values));


            return;
            var srcResponse = "{\"201701\":{\"01\":\"2\",\"02\":\"1\",\"07\":\"2\",\"08\":\"1\",\"14\":\"2\",\"15\":\"1\",\"21\":\"2\",\"27\":\"2\",\"28\":\"2\",\"29\":\"2\",\"30\":\"2\",\"31\":\"1\"},\"201702\":{\"01\":\"2\",\"02\":\"2\",\"05\":\"1\",\"11\":\"1\",\"12\":\"2\",\"18\":\"1\",\"19\":\"2\",\"25\":\"2\",\"26\":\"1\"},\"201703\":{\"04\":\"1\",\"05\":\"2\",\"11\":\"2\",\"12\":\"2\",\"18\":\"1\",\"19\":\"2\",\"25\":\"2\",\"26\":\"1\"},\"201704\":{\"02\":\"1\",\"03\":\"1\",\"04\":\"2\",\"08\":\"2\",\"09\":\"2\",\"15\":\"1\",\"16\":\"1\",\"22\":\"1\",\"23\":\"2\",\"29\":\"1\",\"30\":\"2\"},\"201705\":{\"01\":\"2\",\"06\":\"1\",\"07\":\"2\",\"13\":\"1\",\"14\":\"1\",\"20\":\"1\",\"21\":\"2\",\"28\":\"2\",\"29\":\"2\",\"30\":\"2\"},\"201706\":{\"03\":\"2\",\"04\":\"1\",\"10\":\"2\",\"11\":\"1\",\"17\":\"1\",\"18\":\"2\",\"24\":\"2\",\"25\":\"2\"},\"201707\":{\"01\":\"2\",\"02\":\"1\",\"08\":\"1\",\"09\":\"1\",\"15\":\"1\",\"16\":\"2\",\"22\":\"2\",\"23\":\"2\",\"29\":\"1\",\"30\":\"2\"},\"201708\":{\"05\":\"2\",\"06\":\"1\",\"12\":\"2\",\"13\":\"2\",\"19\":\"2\",\"20\":\"1\",\"26\":\"2\",\"27\":\"1\"},\"201709\":{\"02\":\"1\",\"03\":\"2\",\"09\":\"2\",\"10\":\"1\",\"16\":\"2\",\"17\":\"2\",\"23\":\"1\",\"24\":\"2\"},\"201710\":{\"01\":\"2\",\"02\":\"2\",\"03\":\"2\",\"04\":\"2\",\"05\":\"2\",\"06\":\"1\",\"07\":\"2\",\"08\":\"2\",\"14\":\"1\",\"15\":\"1\",\"21\":\"1\",\"22\":\"1\",\"28\":\"2\",\"29\":\"1\"},\"201711\":{\"04\":\"2\",\"05\":\"1\",\"11\":\"2\",\"12\":\"2\",\"18\":\"1\",\"19\":\"1\",\"25\":\"2\",\"26\":\"2\"},\"201712\":{\"02\":\"1\",\"03\":\"2\",\"09\":\"1\",\"10\":\"2\",\"16\":\"2\",\"17\":\"2\",\"23\":\"1\",\"24\":\"2\",\"30\":\"1\",\"31\":\"1\"}}";

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
                var month = regex_holiday_month.Match(holiday.ToString());
                var dayCollection = regex_holiday_month_day.Matches(holiday.ToString());

                Console.WriteLine("月份：{0}", month.Value);

                foreach (var day in dayCollection)
                {
                    var _day = day.ToString().Replace("\"", "").Split(':');
                    Console.WriteLine("日期：{0} 类型 {1}", _day[0], _day[1]);
                }
            }


            Console.WriteLine("case 1：");
            var regexMonth = new Regex(@"^([1-9]|1[0-2])$");
            for (int i = 0; i <= 13; i++)
            {
                Console.WriteLine("0{0}:{1}-{2}", i, regexMonth.IsMatch(i.ToString()), int.Parse("0" + i));
            }

            Console.WriteLine("case 2：");

            var responseText = "callback( {\"client_id\":\"101223463\",\"openid\":\"162BC9305B40EA41F2FBD5FEF53558A3\",\"unionid\":\"UID_37796DCD0489EC35673DCB2B2F93C747\"} );";

            //匹配unionid
            var regexUnionId = new Regex(@"""unionid"":""(\w+)""", RegexOptions.IgnorePatternWhitespace);

            if (regexUnionId.IsMatch(responseText))
            {
                var responseUnionId = (regexUnionId.Match(responseText).Value ?? "")
                    .Replace("\"", "")
                    .Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                Console.Write(responseUnionId[responseUnionId.Length - 1]);
            }

            Console.WriteLine("case 3：");
            var regexName = new Regex("^([a-zA-Z0-9\u4e00-\u9fa5]{2,20})$");
            Console.WriteLine(regexName.IsMatch("我测试"));

            Console.WriteLine("case 4：");

            var urls = new[] {
                "hTtp://www.baidu.com?fromuid=12446",
                "https://www.sina.com?fromuid=12446"
            };

            var regexUri = new Regex(@"(^((https|http|ftp|rtsp|mms)?:\/\/)[^\s]+)", RegexOptions.IgnoreCase);
            var urlsResult = urls.Where(w => regexUri.IsMatch(w));
            if (urlsResult != null && urlsResult.Any())
            {
                Console.Write(string.Join(",", urlsResult.ToList()));
            }


            if (regexUri.IsMatch("hTtp://www.baidu.com?fromuid=12446"))
            {
                Console.WriteLine("matched");
            }
            else
            {
                Console.WriteLine("not matched");
            }

            var regex = new Regex("([1-9]{1,8}|0)([.][0-9]{1,2})?");
            if (regex.IsMatch("600000.00"))
            {
                Console.WriteLine("is matched");
            }
            else
            {
                Console.WriteLine("not matched");
            }
        }
    }
}
