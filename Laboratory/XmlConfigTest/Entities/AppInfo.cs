using System;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    [Serializable]
    [XmlRoot("app")]
    public class AppInfo
    {
        /// <summary>
        /// 应用标识
        /// </summary>
        [XmlElement("id")]
        public string Id { get; set; }

        /// <summary>
        /// 应用密码
        /// </summary>
        [XmlElement("sercet")]
        public string Sercet { get; set; }
    }
}
