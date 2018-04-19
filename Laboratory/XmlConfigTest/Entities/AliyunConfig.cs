using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// 阿里云相关配置
    /// </summary>
    [Serializable]
    [XmlRoot("aliyun")]
    public sealed class AliyunConfig
    {
        public AliyunConfig()
        {
            this.App = new AliyunApp();
            this.Apis = new List<Api>();
        }

        #region Properties

        /// <summary>
        /// app 信息
        /// </summary>
        [XmlElement("app")]
        public AliyunApp App { get; set; }

        /// <summary>
        /// api接口信息
        /// </summary>
        [XmlArray("apis"), XmlArrayItem("api")]
        public List<Api> Apis { get; set; }

        #endregion

        #region Methods

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

        #endregion
    }

    /// <summary>
    /// 阿里云应用信息
    /// </summary>
    [Serializable]
    public sealed class AliyunApp : AppInfo
    {
        /// <summary>
        /// 应用签名
        /// </summary>
        [XmlElement("signature")]
        public string Signature { get; set; }
    }
}
