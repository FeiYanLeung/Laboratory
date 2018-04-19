using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// 微信配置文件
    /// </summary>
    [Serializable]
    [XmlRoot("wechat")]
    public sealed class WeChatConfig
    {
        /// <summary>
        /// app 信息
        /// </summary>
        [XmlElement("app")]
        public AppInfo App { get; set; }

        /// <summary>
        /// api接口信息
        /// </summary>
        [XmlArray("apis"), XmlArrayItem("api")]
        public List<Api> Apis { get; set; }

        /// <summary>
        /// 获取指定名称的api
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        public Api this[string name]
        {
            get
            {
                if (this.Apis.Count > 0)
                {
                    return this.Apis.Find((w) => name.Equals(w.Name));
                }
                return null;
            }
        }
    }
}
