using System;
using System.Xml.Serialization;

namespace Laboratory.Web.Network.Models
{
    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    public class OfficialReceiveModel
    {
        /// <summary>
        /// 公众号微信号
        /// </summary>
        [XmlElement("ToUserName")]
        public string ToUserName { get; set; }

        /// <summary>
        /// 接收模板消息的用户的openid
        /// </summary>
        [XmlElement("FromUserName")]
        public string FromUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("CreateTime")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// <remarks>
        /// [此处默认:event]
        /// </remarks>
        /// </summary>
        [XmlElement("MsgType")]
        public string MsgType { get; set; }

        /// <summary>
        /// 事件类型
        /// <remarks>
        /// 此处默认:TEMPLATESENDJOBFINISH(模板消息发送结束)
        /// </remarks>
        /// </summary>
        [XmlElement("Event")]
        public string Event { get; set; }

        /// <summary>
        /// 消息id
        /// </summary>
        [XmlElement("MsgID")]
        public string MsgID { get; set; }

        /// <summary>
        /// 消息发送状态
        /// </summary>
        [XmlElement("Status")]
        public string Status { get; set; }
    }
}