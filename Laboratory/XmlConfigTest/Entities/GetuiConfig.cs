using System;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// 个推配置
    /// </summary>
    [Serializable]
    [XmlRoot("getui")]
    public sealed class GetuiConfig
    {
        /// <summary>
        /// 接口地址
        /// </summary>
        [XmlElement("host")]
        public string Host { get; set; }

        /// <summary>
        /// 应用编号
        /// </summary>
        [XmlElement("appId")]
        public string AppId { get; set; }

        /// <summary>
        /// 应用key
        /// </summary>
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// MasterSecret
        /// </summary>
        [XmlElement("master_secret")]
        public string MasterSecret { get; set; }
    }
}
