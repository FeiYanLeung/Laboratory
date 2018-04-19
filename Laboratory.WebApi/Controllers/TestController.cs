using Newtonsoft.Json;
using Laboratory.WebApi.Attributes;
using Laboratory.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Xml.Serialization;
using System.Linq;

namespace Laboratory.WebApi.Controllers
{
    //[BridgeAuthorization]
    public class TestController : ApiController
    {
        [HttpPost]
        public Dictionary<string, string> Index([FromBody]TestParameter parameter)
        {

            if (!ModelState.IsValid)
            {
                return new Dictionary<string, string>()
                {
                    { "error", ModelState.Values.First(w=>w.Errors.Count > 0).Errors[0].ErrorMessage }
                };
            }

            return new Dictionary<string, string>()
            {
                { "access_token", parameter.access_token },
                { "passport_token", parameter.passport_token },
            };
        }


        /// <summary>
        /// XML/JSON转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected T ReceiveParser<T>(Stream stream) where T : class,new()
        {
            try
            {
                using (stream)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var streamContent = reader.ReadToEnd();
                        if (stream.CanSeek) stream.Seek(0, SeekOrigin.Begin);

                        if (streamContent.StartsWith("<xml>"))
                        {
                            var serializer = new XmlSerializer(typeof(T));
                            return serializer.Deserialize(stream) as T;
                        }
                        return JsonConvert.DeserializeObject<T>(streamContent);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return default(T);
        }
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
        /// 消息id
        /// </summary>
        [XmlElement("MsgID")]
        public string MsgID
        {
            get { return MsgId; }
            set
            {
                if (string.IsNullOrEmpty(this.MsgId)) MsgId = value;
            }
        }

        /// <summary>
        /// 消息发送状态
        /// </summary>
        [XmlElement("Status")]
        public string Status { get; set; }
    }
}