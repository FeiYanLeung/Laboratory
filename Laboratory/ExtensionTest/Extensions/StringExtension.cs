using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Laboratory
{
    public static class StringExtension
    {
        public static string DropHTML(this string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring.Replace("&emsp;", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

        /// <summary> 
        /// 获取首字符拼音首字母
        /// </summary> 
        /// <param name="strInput">汉字</param> 
        /// <returns>首字符拼音首字母</returns> 
        public static string PYFirstLetter(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return strInput;
            }
            return toPYFirstLetter(strInput.Substring(0, 1));
        }

        /// <summary>
        /// 全拼
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>相对应的汉语拼音首字母串集合</returns>
        public static List<string> PYFirstLetters(this string strInput)
        {
            var builder = new List<string>();

            foreach (char ch in strInput)
            {
                if (ChineseChar.IsValidChar(ch))
                {
                    var chChar = new ChineseChar(ch);
                    builder.Add(chChar.Pinyins[0]);
                }
                else
                {
                    builder.Add(ch.ToString());
                }
            }
            return builder;
        }

        /// <summary>
        /// 计算汉字笔画
        /// </summary>
        /// <param name="strInput">要检测的汉字</param>
        /// <returns></returns>
        public static List<KeyValuePair<char, int>> StrokeNumber(this string strInput)
        {
            var stroke = new List<KeyValuePair<char, int>>();
            foreach (char ch in strInput)
            {
                if (ChineseChar.IsValidChar(ch))
                {
                    var chChar = new ChineseChar(ch);
                    stroke.Add(new KeyValuePair<char, int>(ch, chChar.StrokeNumber));
                }
            }
            return stroke;
        }

        /// <summary>
        /// 转换为Base64编码的字符串
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string ToBase64(this string strInput)
        {
            byte[] input_bytes = Encoding.Default.GetBytes(strInput);
            return Convert.ToBase64String(input_bytes);
        }

        /// <summary>
        /// 将Base64编码的字符串转换为可识别状态
        /// </summary>
        /// <param name="strEncoded">Base64编码后的字符串</param>
        /// <returns></returns>
        public static string FromBase64(this string strEncoded)
        {
            byte[] output_bytes = Convert.FromBase64String(strEncoded);
            return Encoding.Default.GetString(output_bytes);
        }

        /// <summary> 
        /// 汉字转化为拼音首字母
        /// </summary> 
        /// <param name="strInput">汉字</param> 
        /// <returns>首字母</returns> 
        private static string toPYFirstLetter(string strInput)
        {
            var builder = new StringBuilder();
            foreach (char ch in strInput)
            {
                if (ChineseChar.IsValidChar(ch))
                {
                    var chChar = new ChineseChar(ch);
                    builder.Append(chChar.Pinyins[0].Substring(0, 1));
                }
                else
                {
                    builder.Append(ch.ToString());
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 返回处理后的十六进制字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns>hex</returns>
        public static string ToHex(this string input)
        {
            return BitConverter.ToString(ASCIIEncoding.Default.GetBytes(input)).Replace("-", "");
        }

        /// <summary>
        /// 返回十六进制代表的字符串
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexToString(this string hex)
        {
            hex = hex.Replace(" ", "");
            if (hex.Length <= 0) return "";

            byte[] vBytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                if (!byte.TryParse(hex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                {
                    vBytes[i / 2] = 0;
                }
            }
            return ASCIIEncoding.Default.GetString(vBytes);
        }
    }
}
