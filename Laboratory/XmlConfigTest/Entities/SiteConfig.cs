using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Laboratory.XmlConfigTest.Entities
{
    /// <summary>
    /// 站点配置
    /// </summary>
    [Serializable]
    [XmlRoot("site")]
    public class SiteConfig
    {
        public SiteConfig()
        {
            this.Domains = new List<Domain>();
        }

        /// <summary>
        /// 消息心跳频率
        /// </summary>
        private int _messageInterval = 10;
        [XmlElement("msginterval")]
        public int MessageInterval
        {
            get { return _messageInterval; }
            set { _messageInterval = value; }
        }

        /// <summary>
        /// 文件上传最大大小
        /// </summary>
        private double _maxFileSize = 2.0;
        [XmlElement("maxfilesize")]
        public double MaxFileSize
        {
            get { return _maxFileSize; }
            set { _maxFileSize = value; }
        }

        /// <summary>
        /// 默认图片
        /// </summary>
        [XmlElement("defaultpic")]
        public string DefaultPic { get; set; }

        /// <summary>
        /// 服务器域名信息
        /// </summary>
        [XmlArray("domains"), XmlArrayItem("domain")]
        public List<Domain> Domains { get; set; }

        /// <summary>
        /// 对称加密Salt
        /// </summary>
        private string _sha1salt = "lottak";
        [XmlElement("sha1salt")]
        public string SHA1Salt
        {
            get { return _sha1salt; }
            set { _sha1salt = value; }
        }

        /// <summary>
        /// 系统版本
        /// </summary>
        [XmlElement("version")]
        public string Version { get; set; }

        #region 邮件设置

        /// <summary>
        /// 邮件
        /// </summary>
        [XmlElement("email")]
        public EmailConfig Email { get; set; }

        #endregion

        #region 消息队列

        /// <summary>
        /// 消息队列服务
        /// </summary>
        [XmlElement("msmq")]
        public string MSMQ { get; set; }

        #endregion

        #region IM

        /// <summary>
        /// IM服务器地址
        /// </summary>
        [XmlElement("im")]
        public IMConfig IM { get; set; }

        #endregion
    }

    #region  include

    /// <summary>
    /// 域名信息
    /// </summary>
    [XmlRoot("domain")]
    public sealed class Domain
    {
        /// <summary>
        /// 域名名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 域名地址
        /// </summary>
        [XmlAttribute("url")]
        public string Url { get; set; }
    }

    /// <summary>
    /// 邮件
    /// </summary>
    [Serializable]
    [XmlRoot("email")]
    public sealed class EmailConfig
    {
        /// <summary>
        /// 邮件smtp服务器地址
        /// </summary>
        [XmlElement("smtp")]
        public string SMTP { get; set; }

        /// <summary>
        /// 是否启用SSL加密连接
        /// </summary>
        private bool _ssl = true;
        [XmlElement("ssl")]
        public bool SSL
        {
            get { return _ssl; }
            set { _ssl = value; }
        }

        /// <summary>
        /// SMTP端口
        /// </summary>
        private int _port = 25;
        [XmlElement("port")]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 发件人地址
        /// </summary>
        [XmlElement("from")]
        public string From { get; set; }

        /// <summary>
        /// 邮箱账号
        /// </summary>
        [XmlElement("username")]
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        [XmlElement("password")]
        public string Password { get; set; }

        /// <summary>
        /// 发件人昵称
        /// </summary>
        [XmlElement("nickname")]
        public string NickName { get; set; }
    }

    /// <summary>
    /// IM
    /// </summary>
    [Serializable]
    [XmlRoot("im")]
    public sealed class IMConfig
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        [XmlElement("host")]
        public string Host { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        private int _port = 5222;
        [XmlElement("port")]
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
    }

    #endregion
}
