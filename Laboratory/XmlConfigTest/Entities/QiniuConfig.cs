using System;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// 七牛配置
    /// </summary>
    [Serializable]
    [XmlRoot("qiniu")]
    public class QiniuConfig
    {
        /// <summary>
        /// AccessKey
        /// </summary>
        [XmlElement("access_key")]
        public string AccessKey { get; set; }

        /// <summary>
        /// SecretKey
        /// </summary>
        [XmlElement("secret_key")]
        public string SecretKey { get; set; }

        /// <summary>
        /// Bucket
        /// </summary>
        [XmlElement("bucket")]
        public string Bucket { get; set; }
    }
}
