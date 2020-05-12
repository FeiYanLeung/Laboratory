using System.Globalization;
using System.Text;

namespace Laboratory
{
    public static class DigitExtension
    {
        /// <summary>
        /// 将数字转换为艺术字体(1->①)
        /// </summary>
        /// <param name="srcDigit"></param>
        /// <returns></returns>
        public static string ToArtDigit(this int srcDigit)
        {
            // 转换为Unicode/(Hex)
            // 转换为Int(DEC)
            // Operate
            // 转换为Hex

            var artBuffer = Encoding.Unicode.GetBytes("⓪");


            var sb = new StringBuilder();
            foreach (var digit in srcDigit.ToString())
            {
                var buffer = Encoding.Unicode.GetBytes(digit.ToString());
                byte[] bytes = {
                   byte.Parse(artBuffer[0].ToString()),
                    artBuffer[1]
                };

                sb.AppendFormat(Encoding.Unicode.GetString(bytes));
            }
            return sb.ToString();
        }
    }
}
