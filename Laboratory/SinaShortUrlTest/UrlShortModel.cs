using Newtonsoft.Json;
using System;

namespace Laboratory.SinaShortUrlTest
{
    #region 短网址对象实体（嵌套类）

    /// <summary>
    /// 短网址实体类
    /// </summary>
    [Serializable]
    public class UrlShortModel
    {
        /// <summary>
        /// 短域名
        /// </summary>
        [JsonProperty("url_short")]
        public virtual string UrlShort { get; set; }

        /// <summary>
        /// 长域名
        /// </summary>
        [JsonProperty("url_long")]
        public virtual string UrlLong { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty("type")]
        public virtual int Type { get; set; }
    }

    #endregion
}
