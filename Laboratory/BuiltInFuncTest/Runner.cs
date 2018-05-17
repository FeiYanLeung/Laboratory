using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Laboratory.BuiltInFuncTest
{
    public class Runner : IRunner
    {
        public string Name
        {
            get { return "内置函数"; }
        }

        [Serializable]
        public class TempItem
        {
            public TempItem() { }
            public TempItem(int _id)
            {
                this.id = _id;
            }
            public int id { get; set; }
        }

        [Serializable]
        public class Temp : ICloneable
        {
            public int id { get; set; }

            public string name { get; set; }

            /// <summary>
            /// 使用浅表复制时，这个对象的id永远为最后一次设置的值
            /// </summary>
            public TempItem item { get; set; }

            /// <summary>
            /// 浅表拷贝
            /// </summary>
            /// <returns></returns>
            public object Clone()
            {
                return base.MemberwiseClone();
            }

            /// <summary>
            /// 深度拷贝
            /// </summary>
            /// <returns></returns>
            public Temp DeepClone()
            {
                using (var ms = new MemoryStream())
                {
                    XmlSerializer serializer = new XmlSerializer(this.GetType());
                    serializer.Serialize(ms, this);
                    ms.Seek(0, SeekOrigin.Begin);
                    return (Temp)serializer.Deserialize(ms);
                }
            }
        }

        public bool IsValid(object value, int count)
        {
            if (value is System.String)
            {
                return value.ToString().Length >= count;
            }
            else if (value is System.Collections.IEnumerable)
            {
                var valueable = value as System.Collections.IEnumerable;
                var valuerator = valueable.GetEnumerator();
                int i = 0;

                while (valuerator.MoveNext())
                {
                    if (++i > count) break;
                }

                return i >= count;
            }
            return false;
        }

        /// <summary>
        /// 位运算符
        /// </summary>
        private void bitOperator()
        {
            //&:按位"与"，true & true = true; true & false = false; false & false = false;
            //|:按位"或"，true | true = true; true | false = true; false | false = false;

            #region 按位"与"

            Console.WriteLine($"true&true:{true & true}");
            Console.WriteLine($"true&false:{true & false}");
            Console.WriteLine($"false&false:{false & false}");

            #endregion

            #region 按位"或"

            Console.WriteLine($"true|true:{true | true}");
            Console.WriteLine($"true|false:{true | false}");
            Console.WriteLine($"false|false:{false | false}");

            #endregion

            int n1 = 73 << 3;    // = 7 * Math.Pow(2, 3) = 7*(2*2*2) =584(十进制)
            Console.WriteLine($"n1:{n1}");

            //byte：1byte = 8bit
            //bit：(从右往左)前7位表示数值，第8位是符号位（0为正，1为负）,所以最大正数为0 1111111=127,最大负数为1 1111111=-127，所以取值范围为-127~127共256个数（存在-0和+0）。

            // step1: 将584转换为二进制=1001001000
            // step2: 将二进制转换为十进制=Math.Pow(2, 3)+Math.Pow(2, 6)+Math.Pow(2, 9)，即从右往左取数字为1的下标，得到下标3，6，9，分别取其对应下标的2的n次方。
            // step3: 由于Math.Pow(2, 9)等于512大于255,舍弃得出=Math.Pow(2, 3)+Math.Pow(2, 6) = 72
            var n2 = (byte)n1;
            Console.WriteLine($"n2:{n2}, {n1 & 255}");

            return;
        }

        /// <summary>
        /// 格式化TimeSpan
        /// </summary>
        /// <param name="ts"></param>
        /// <param name="defValue">当时间间隔小于1分钟时显示的内容</param>
        /// <returns></returns>
        private string fmtTs(TimeSpan ts, string defValue = "0")
        {
            if (ts.TotalMinutes > 0)
            {
                var builder = new StringBuilder();
                if (ts.TotalDays > 0 && ts.Days > 0) builder.Append($"{ts.Days}天");
                if (ts.TotalHours > 0 && ts.Hours > 0) builder.Append($"{ts.Hours}小时");
                if (ts.TotalMinutes > 0 && ts.Minutes > 0) builder.Append($"{ts.Minutes}分钟");

                return builder.ToString();
            }
            return defValue;
        }

        public void Run()
        {
            Console.WriteLine(this.fmtTs(DateTime.Now.AddHours(24.3) - DateTime.Now));

            return;
            var bnum = intToByte(10);
            Console.WriteLine(string.Join("", bnum));

            Console.WriteLine(byteToInt(bnum));

            return;
            this.bitOperator();

            var v1 = new List<int>() { 1 };
            var v2 = "1";

            Console.WriteLine(IsValid(v1, 2));
            Console.WriteLine(IsValid(v2, 2));


            return;


            DateTime? dtNow = DateTime.Now;

            Console.WriteLine(dtNow?.ToUniversalTime());

            Console.WriteLine(DateTime.ParseExact("20180412000001", "yyyyMMddHHmmss", CultureInfo.CurrentCulture));
            return;

            return;
            var models = new List<Temp>();
            Temp tmp = new Temp()
            {
                item = new TempItem(-1)
            };

            for (int i = 0; i < 5; i++)
            {
                var tmp1 = tmp.DeepClone();// as Temp;
                tmp1.id = i;
                tmp1.name = "name" + i;
                tmp1.item.id = i;

                models.Add(tmp1);
            }

            foreach (var item in models)
            {
                Console.WriteLine($"{item.id} {item.name} {item.item.id}");
            }


            return;


            var newId = Guid.NewGuid();

            Console.WriteLine(newId.ToString("n"));    //带有连接符"-"的32位数字
            Console.WriteLine(newId.ToString("d"));    //默认格式,32数字
            Console.WriteLine(newId.ToString("b"));    //带有连接符"-"的32位数字,并使用花括号"{}"作为开始和结束
            Console.WriteLine(newId.ToString("p"));    //带有连接符"-"的32位数字,并使用大括号"()"作为开始和结束
            Console.WriteLine(newId.ToString("x"));    //前三个16进制的数字表示前16位,第4个值(即值内花括号包含的8个16进制的数值)表示后16位,使用花括号"{}"作为开始和结束

            Console.WriteLine(DateTime.Now.AddDays(-1).ToShortDateString());
            Console.WriteLine(DateTime.Now.Date);
            return;
            this.divisionDts();
            this.divisionDts2();
            this.charset();
            this.valueScope();
            this.consoleOut();
            this.dateDiff();
            this.doubleDimensionalArray();
            this.replacePath();
            this.compareVersion();
            this.eachDateTime();
            this.sortedDictionary();
            this.randCode();
            this.md5("escape", Encoding.Default);
            this.other();
            this.xor();
            this.unsafeFib();
        }

        /// <summary>
        /// 10进制转换为2进制
        /// </summary>
        /// <param name="number">10进制数字</param>
        /// <returns></returns>
        public byte[] intToByte(int number)
        {
            var stack = new Stack<byte>();
            do
            {
                stack.Push((byte)((number % 2) & 255));
                number = number / 2;
            } while (number >= 1);

            return stack.ToArray();
        }

        /// <summary>
        /// 2进制转换10进制
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public double byteToInt(byte[] bytes_digit)
        {
            Stack<byte> stack = new Stack<byte>(bytes_digit.Length);
            foreach (byte digit in bytes_digit)
            {
                stack.Push(digit);
            }

            double dest_digit = 0d;
            for (int i = 0; i < stack.Count; i++)
            {
                if (0.Equals(stack.ElementAt(i))) continue;
                dest_digit += Math.Pow(2, i);
            }
            return dest_digit;
        }

        /// <summary>
        /// [struct]取值范围
        /// </summary>
        private void valueScope()
        {
            Console.WriteLine("byte:  Min:{0}  Max:{1}", byte.MinValue, byte.MaxValue);
            Console.WriteLine("int:  Min:{0}  Max:{1}", int.MinValue, int.MaxValue);
            Console.WriteLine("uint:  Min:{0}  Max:{1}", uint.MinValue, uint.MaxValue);
            Console.WriteLine("double:  Min:{0}  Max:{1}", double.MinValue, double.MaxValue);
            Console.WriteLine("float:  Min:{0}  Max:{1}", float.MinValue, float.MaxValue);
            Console.WriteLine("decimal:  Min:{0}  Max:{1}", decimal.MinValue, decimal.MaxValue);
            Console.WriteLine("DateTime:  Min:{0}  Max:{1}", DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 字符长度以及占用字节长度
        /// </summary>
        private void charset()
        {
            Console.WriteLine("一个Byte = 8bit"); //一个字节(别名：8位元组)等于8比特

            var chinese = "你好";

            Console.WriteLine("“{0}”的字符长度为{1}，UTF-32编码时字节长度为{2}", chinese, chinese.Length, Encoding.UTF32.GetByteCount(chinese));   //8[每个字符为4字节]
            Console.WriteLine("“{0}”的字符长度为{1}，UTF-8编码时字节长度为{2}", chinese, chinese.Length, Encoding.UTF8.GetByteCount(chinese)); //6[每个字符为3字节]
            Console.WriteLine("“{0}”的字符长度为{1}，Unicode编码时字节长度为{2}", chinese, chinese.Length, Encoding.Unicode.GetByteCount(chinese));    //4[每个字符为2字节]
            Console.WriteLine("“{0}”的字符长度为{1}，ASCII编码时字节长度为{2}", chinese, chinese.Length, Encoding.ASCII.GetByteCount(chinese));    //2[每个字符为1字节]


            var english = "hello";
            Console.WriteLine("“{0}”的字符长度为{1}，UTF-32编码时字节长度为{2}", english, english.Length, Encoding.UTF32.GetByteCount(english));   //20[每个字符为4字节]
            Console.WriteLine("“{0}”的字符长度为{1}，UTF-8编码时字节长度为{2}", english, english.Length, Encoding.UTF8.GetByteCount(english)); //5[每个字符为1字节]
            Console.WriteLine("“{0}”的字符长度为{1}，Unicode编码时字节长度为{2}", english, english.Length, Encoding.Unicode.GetByteCount(english));  //10[每个字符为2字节]
            Console.WriteLine("“{0}”的字符长度为{1}，ASCII编码时字节长度为{2}", english, english.Length, Encoding.ASCII.GetByteCount(english));    //5[每个字符为1字节]
        }

        /// <summary>
        /// 分割时间/需要是同一日期
        /// </summary>
        /// <param name="st">起始时间</param>
        /// <param name="et">截止时间</param>
        /// <param name="lts">排除的时间</param>
        /// <remarks>如果st-et和lts之前存在交集，则分割中间的时间</remarks>
        private void divisionDts()
        {
            var st = DateTime.Parse("2018-3-2 8:00");//处理后的请求查询开始时间
            var et = DateTime.Parse("2018-3-2 17:30");

            Console.WriteLine("");
            Console.WriteLine("=>{0:HH:mm} {1:HH:mm}, 共：{2} s.", st, et, (et - st).TotalSeconds);
            var dts = new List<DateTime[]>();

            #region 休息时间
            {
                var lts = new List<DateTime[]>() {
                    new DateTime[2]{ DateTime.Parse("2018-3-2 11:50"), DateTime.Parse("2018-3-2 12:30") },
                    new DateTime[2]{ DateTime.Parse("2018-3-2 15:00"), DateTime.Parse("2018-3-2 15:20") },
                    new DateTime[2]{ DateTime.Parse("2018-3-2 10:00"), DateTime.Parse("2018-3-5 13:30") }
                };

                Console.WriteLine("==========");
                dts.Clear();
                foreach (var lt in lts)
                {
                    if (st <= lt[1] && et >= lt[0])//(st.Compare(lt[1], true, false).TotalSeconds <= 0 && et.Compare(lt[0], true, false).TotalSeconds >= 0)
                    {
                        if (st.Compare(lt[0], true, false).TotalSeconds <= 0 && et.Compare(lt[1], true, false).TotalSeconds >= 0)
                        {
                            dts.Add(new DateTime[] { lt[0], lt[1] });
                            continue;
                        }
                        if (st.Compare(lt[0], true, false).TotalSeconds <= 0)//(st < lt[0])
                        {
                            if (et.Compare(lt[1], true, false).TotalSeconds >= 0) dts.Add(lt);  //(et >= lt[1]) 
                            else dts.Add(new DateTime[] { lt[0], et });
                        }
                        else if (st.Compare(lt[0], true, false).TotalSeconds >= 0 && st.Compare(lt[1], true, false).TotalSeconds <= 0) //(st > lt[0] && st <= lt[1])
                        {
                            dts.Add(new DateTime[] { st, lt[1] });
                        }
                    }
                }

                foreach (var dt in dts)
                {
                    Console.WriteLine("{0:HH:mm} {1:HH:mm}", dt[0], dt[1]);
                }

                var total_ts = et.Compare(st, true, false).TotalSeconds;
                var total_lts = dts.Select(q => q[1].Compare(q[0], true, false)).Sum(s => s.TotalSeconds);

                Console.WriteLine("总时长=>{0} s.", total_ts);
                Console.WriteLine("总休息时长=>{0} s.", total_lts);
                Console.WriteLine("总工作时长=>{0} s.", total_ts - total_lts);
            }
            #endregion
        }


        /// <summary>
        /// 分割时间/需要是同一日期
        /// </summary>
        /// <param name="st">起始时间</param>
        /// <param name="et">截止时间</param>
        /// <param name="lts">排除的时间</param>
        /// <remarks>如果st-et和lts之前存在交集，则分割中间的时间</remarks>
        private void divisionDts2()
        {
            var st = DateTime.Parse("2018-3-2 08:00");//处理后的请求查询开始时间
            var et = DateTime.Parse("2018-3-2 19:00");

            Console.WriteLine("");
            Console.WriteLine("=>{0:HH:mm} {1:HH:mm}", st, et);
            var dts = new List<DateTime[]>();

            #region 工作时长(lts中的每一项都会被记做一天。即即使lts中的项都是同一天也会出现重复计算的问题)
            {
                var lts = new List<DateTime[]>() {
                    new DateTime[2]{ DateTime.Parse("2018-3-2 11:50"), DateTime.Parse("2018-3-2 12:30") },
                    new DateTime[2]{ DateTime.Parse("2018-3-2 15:00"), DateTime.Parse("2018-3-2 15:20") },
                    new DateTime[2]{ DateTime.Parse("2018-3-2 17:00"), DateTime.Parse("2018-3-2 17:30") }
                };

                foreach (var lt in lts)
                {
                    var pst = st;
                    var pet = et;
                    if ((pst.Compare(lt[0], true, false).TotalSeconds <= 0 && pet.Compare(lt[0], true, false).TotalSeconds <= 0)
                        || (pst.Compare(lt[1], true, false).TotalSeconds >= 0 && pet.Compare(lt[1], true, false).TotalSeconds >= 0))
                    {
                        dts.Add(new DateTime[2] { pst, pet });
                        pst = pet;
                    }

                    if (pst.Compare(lt[0], true, false).TotalSeconds <= 0 && pet.Compare(lt[0], true, false).TotalSeconds >= 0)
                    {
                        dts.Add(new DateTime[2] { pst, lt[0] });
                        pst = lt[0];
                    }

                    if (pst.Compare(pet, true, false).TotalSeconds <= 0 && lt[1].Compare(pet, true, false).TotalSeconds <= 0)
                    {
                        dts.Add(new DateTime[2] { lt[1], pet });
                    }
                }

                foreach (var dt in dts)
                {
                    Console.WriteLine("{0:HH:mm} {1:HH:mm}", dt[0], dt[1]);
                }

                Console.WriteLine("总工作时长=>{0} s.", dts.Select(q => q[1].Compare(q[0], true, false)).Sum(s => s.TotalSeconds));

            }
            #endregion
        }

        /// <summary>
        /// 时间间隔
        /// </summary>
        private void dateDiff()
        {
            var ost = DateTime.Parse("2018-1-29 11:50:00");
            var oet = DateTime.Parse("2018-1-31 14:40:00");

            var dtarr = new List<DateTime[]>();
            for (DateTime dt = ost, dt1 = oet; dt < dt1; dt = dt.AddDays(1))
            {
                var dta1 = DateTime.Parse(dt.ToString("yy/MM/dd"));
                var dta2 = DateTime.Parse(String.Format("{0:yy/MM/dd 23:59:59}", dt));

                var builder = String.Format("{0:yy/MM/dd HH:mm:ss} - {0:yy/MM/dd 23:59:59}", dt);
                if (dt == ost)
                {
                    dta1 = DateTime.Parse(dt.ToString());
                    dt = DateTime.Parse(dt.ToString("yyyy/MM/dd"));
                }
                if (dt.DayOfYear == oet.DayOfYear && dt.DayOfWeek == oet.DayOfWeek)
                {
                    dta2 = oet;
                }
                dtarr.Add(new DateTime[2] { dta1, dta2 });
            }

            var lunch_times = new List<DateTime[]>
            {
                new DateTime[]{ DateTime.Parse("2018/1/29 12:00:00"), DateTime.Parse("2018/1/29 13:00:00") },   //11:50:00 - 23:59:59 = 21
                new DateTime[]{ DateTime.Parse("2018/1/29 13:00:00"), DateTime.Parse("2018/1/29 13:30:00") },
                new DateTime[]{ DateTime.Parse("2018/1/29 13:30:00"), DateTime.Parse("2018/1/29 14:30:00") }
            };

            foreach (var item in dtarr)
            {
                var st = item[0];
                var et = item[1];

                var ts = et - st;
                Console.WriteLine("【origin： {0:yy/MM/dd HH:mm:ss} - {1:yy/MM/dd HH:mm:ss}】", st, et);
                Console.WriteLine("【origin： 时间相隔：{0}小时{1}分钟{2}秒 】", ts.Hours, ts.Minutes, ts.Seconds);

                foreach (var lt in lunch_times)
                {
                    var clts = DateTime.Parse(String.Format("{0:yyyy/MM/dd} {1:HH:mm:ss}", st, lt[0]));
                    var clte = DateTime.Parse(String.Format("{0:yyyy/MM/dd} {1:HH:mm:ss}", st, lt[1]));

                    if (clts <= et && clte >= st)
                    {
                        if (clte > et) clte = et;
                        if (st > clts) clts = st;

                        ts -= clte - clts;
                    }
                }

                var sumt = (et - st - ts);
                Console.WriteLine("休息时间：{0}小时{1}分钟{2}秒 ", sumt.Hours, sumt.Minutes, sumt.Seconds);
                Console.WriteLine("工作时间：{0}小时{1}分钟{2}秒 ", ts.Hours, ts.Minutes, ts.Seconds);
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// 二维数组
        /// </summary>
        private void doubleDimensionalArray()
        {
            //数组长度限制为1,2
            int[,] arr = new int[1, 2];
            arr[0, 0] = 0x00000;
            arr[0, 1] = 0x01;
            arr[1, 0] = 0x010;    //error   //组下标(第一个下标)超过界限
            arr[0, 2] = 0x012;    //error   //组内下标(第二个下标)超过界限

            Console.WriteLine(arr[0, 0]);
        }

        /// <summary>
        /// 控制台信息输出到文件
        /// </summary>
        private void consoleOut()
        {
            var tsw = Console.Out;

            using (var sw = new StreamWriter(@".\consoleOut.log"))
            {
                Console.SetOut(sw);

                Console.WriteLine("Here is the result:");
                Console.WriteLine("Processing......");

                sw.Flush();
            }
            Console.SetOut(tsw);
            Console.WriteLine("OK!");
        }

        /// <summary>
        /// 路径替换
        /// </summary>
        private void replacePath()
        {
            var settingValue = @"\configs\site.config";
            var separator = settingValue.Contains("/") ? "/" : "\\";
            var settings = settingValue.Split(separator[0]).ToList();

            Console.WriteLine(String.Join(separator, settings));

            settings.Insert(settings.Count - 1, "debug");

            Console.WriteLine(String.Join(separator, settings));
        }

        /// <summary>
        /// 版本号对比
        /// </summary>
        private void compareVersion()
        {
            var version = "5.1.171127".Split('.');
            var app_main_verison = int.Parse(version[0] + version[1]);

            if (app_main_verison >= 51)
            {
                var regex = new Regex(@"\d{2}");
                var dts = regex.Matches(version[2]);
                int app_file_version_y = int.Parse("20" + dts[0].Value),
                    app_file_version_m = int.Parse(dts[1].Value),
                    app_file_version_d = int.Parse(dts[2].Value);
                var app_file_version = new DateTime(app_file_version_y, app_file_version_m, app_file_version_d);

                var app_file_version_ts = app_file_version.ToUnixTimestamp();

                var sversion = DateTime.Parse("2017/11/28").ToUnixTimestamp();
                if (app_file_version_ts < sversion)
                {
                    Console.WriteLine("不显示");
                }
                else
                {
                    Console.WriteLine("显示");
                }
            }
        }

        /// <summary>
        /// 按ascii排序的字典对象
        /// </summary>
        private void sortedDictionary()
        {
            var sortDict = new SortedDictionary<string, object>(){
                {"timestamp","1"},
                {"jsapi_ticket","2"},
                {"noncestr","3"},
                {"url","4"}
            };

            Console.WriteLine("ascii 排序后的信息：");
            foreach (var item in sortDict)
            {
                Console.WriteLine("{0}=>{1}", item.Key, item.Value);
            }
        }

        /// <summary>
        /// 循环时间范围 
        /// </summary>
        private void eachDateTime()
        {
            var dt1 = DateTime.Now.AddYears(-1);
            var dt2 = DateTime.Now.AddMonths(6);

            Console.WriteLine("循环时间范围：{0:yyyy/MM/dd}~{1:yyyy/MM/dd}", dt1, dt2);

            for (DateTime i = dt1, j = dt2; i < j; i = i.AddMonths(1))
            {
                Console.WriteLine(i.ToString("yyyyMM"));
            }
        }

        /// <summary>
        /// 随机生成x位的字符串
        /// </summary>
        /// <returns></returns>
        private void randCode(int randLen = 8)
        {
            var rand = new Random(unchecked((int)DateTime.Now.Ticks));
            var chars = "abcdefghijkmnpqrstuvwxyz23456789";

            string noncestr = string.Empty;

            while (noncestr.Length < randLen)
            {
                var randn = rand.Next(0, chars.Length - 1);
                noncestr = string.Concat(noncestr, chars[randn]);
            }

            Console.WriteLine("本次生成的{0}位随机数为：{1}", randLen, noncestr);
        }

        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private void md5(string cipherText, Encoding encode)
        {
            var crypto = new MD5CryptoServiceProvider();
            var bytes = encode.GetBytes(cipherText);
            bytes = crypto.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var num in bytes)
            {
                sb.AppendFormat("{0:x2}", num);
            }
            Console.WriteLine("对{0}md5加密后的结果为：{1}", cipherText, sb.ToString().ToLower());
        }

        /// <summary>
        /// 异或运算
        /// </summary>
        private void xor()
        {
            /**
            10001010
            ^
            00110011
            =
            10111001
            */



            long result = gcd(15, 45);
            Console.Write(result);

        }

        /// <summary>
        /// 最大公约数
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long gcd(long m, long n)
        {
            while (n != 0)
            {
                long rem = m % n;
                m = n;
                n = rem;
            }
            return m;
        }

        /// <summary>
        /// 其他
        /// </summary>
        private void other()
        {
            #region ~ 按位求补运算

            int[] values = { 0, 0x111, 0xfffff, 0x8888, 0x22000022 };
            foreach (int v in values)
            {
                Console.WriteLine("~0x{0:x8} = 0x{1:x8}", v, ~v);
            }

            /*
            Output:
            ~0x00000000 = 0xffffffff
            ~0x00000111 = 0xfffffeee
            ~0x000fffff = 0xfff00000
            ~0x00008888 = 0xffff7777
            ~0x22000022 = 0xddffffdd
            */

            #endregion

            #region ^ 异或运算
            // 数学描述：异或是Zz（小z为2）群的加法运算，满足加法结合律和交换律
            // 通俗解释：满足两个条件之一，不能两个都选，也不能一个不选
            // 在二进制中，则表示两个数的差异，例如(10001010^00110011)=10111001

            // Logical exclusive-OR

            // When one operand is true and the other is false, exclusive-OR 
            // returns True.
            Console.WriteLine(true ^ false);
            // When both operands are false, exclusive-OR returns False.
            Console.WriteLine(false ^ false);
            // When both operands are true, exclusive-OR returns False.
            Console.WriteLine(true ^ true);


            // Bitwise exclusive-OR

            // Bitwise exclusive-OR of 0 and 1 returns 1.
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x0 ^ 0x1, 2));
            // Bitwise exclusive-OR of 0 and 0 returns 0.
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x0 ^ 0x0, 2));
            // Bitwise exclusive-OR of 1 and 1 returns 0.
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x1 ^ 0x1, 2));

            // With more than one digit, perform the exclusive-OR column by column.
            //    10
            //    11
            //    --
            //    01
            // Bitwise exclusive-OR of 10 (2) and 11 (3) returns 01 (1).
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x2 ^ 0x3, 2));

            // Bitwise exclusive-OR of 101 (5) and 011 (3) returns 110 (6).
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x5 ^ 0x3, 2));

            // Bitwise exclusive-OR of 1111 (decimal 15, hexadecimal F) and 0101 (5)
            // returns 1010 (decimal 10, hexadecimal A).
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0xf ^ 0x5, 2));

            // Finally, bitwise exclusive-OR of 11111000 (decimal 248, hexadecimal F8)
            // and 00111111 (decimal 63, hexadecimal 3F) returns 11000111, which is 
            // 199 in decimal, C7 in hexadecimal.
            Console.WriteLine("Bitwise result: {0}", Convert.ToString(0xf8 ^ 0x3f, 2));

            #endregion

            #region 运算符的简写形式
            //(x /= y) = (x = x / y)    //除法
            //(x %= y) = (x = x % y)    //求余
            //(x *= y) = (x = x * y)    //乘法
            //(x -= y) = (x = x - y)    //减法
            //(x += y) = (x = x + y)    //加法

            #endregion

            object objectId = Guid.NewGuid();
            object strObjectId = objectId.ToString();

            Console.WriteLine("object is Guid：{0}", objectId is Guid);
            Console.WriteLine("object.ToString() is Guid：{0}", strObjectId is Guid);
            Console.WriteLine("HttpMethod：{0}", HttpMethod.Get.Method);
        }

        /// <summary>
        /// unsafe+fixed，输出的结果是一个斐波拉契数(release运行时会内存溢出)
        /// <remarks>
        /// 当类型为decimal时，第99位输出结果为： 354224848179261915075
        /// 当类型为double类型时，第99位输出结果为： 3.54224848179262E+20
        /// </remarks>
        /// </summary>
        unsafe void unsafeFib()
        {
            double[] nums = { 0, 1, 2, 3, 4, 5 };
            fixed (double* p = nums)
            {
                p[0] = p[1] = 1;
                Console.WriteLine("0=>1");
                Console.WriteLine("1=>1");
                for (int i = 2; i < 100; ++i)
                {
                    p[i] = p[i - 1] + p[i - 2];
                    Console.WriteLine("{0}=>{1}", i, p[i]);
                }
            }
        }
    }
}
