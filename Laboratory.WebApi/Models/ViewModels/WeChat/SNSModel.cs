using System;
using System.Xml.Serialization;

namespace Laboratory.WebApi.Models.ViewModels.WeChat
{
    /// <summary>
    /// 授权
    /// </summary>
    [Serializable]
    public class SNSToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }

    }

    /// <summary>
    /// 用户信息
    /// </summary>
    [Serializable]
    public class SNSUserInfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 同一微信应用下，用户唯一标示
        /// </summary>
        public string unionid { get; set; }
    }

    /// <summary>
    /// 服务器推送接受
    /// </summary>
    [Serializable]
    [XmlRoot("xml", Namespace = "")]
    public class SNSReceiveModel
    {
        /// <summary>
        /// 公众号微信号
        /// </summary>
        [XmlElement("ToUserName")]
        public string ToUserName { get; set; }

        /// <summary>
        /// 接收模板消息的用户的OpenId
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
        [XmlElement("MsgId")]
        public string MsgId { get; set; }

        /// <summary>
        /// 消息发送状态
        /// </summary>
        [XmlElement("Status")]
        public string Status { get; set; }
    }
}