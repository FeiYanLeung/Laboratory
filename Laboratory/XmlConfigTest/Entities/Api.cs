using System;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// api详情
    /// </summary>
    [Serializable]
    public class Api
    {
        /// <summary>
        /// api调用标识名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// api地址
        /// </summary>
        [XmlAttribute("url")]
        public string Url { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlAttribute("description")]
        public string Description;
    }
}
