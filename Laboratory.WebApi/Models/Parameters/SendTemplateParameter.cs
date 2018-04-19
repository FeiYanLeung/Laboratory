using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.WebApi.Models.Parameters
{
    /// <summary>
    /// 模板消息
    /// </summary>
    [Serializable]
    public class SendTemplateParameter
    {
        public SendTemplateParameter()
        {
            this.data = new Dictionary<string, SendTemplateDataParameter>();
        }
        /// <summary>
        /// 用户OpenId
        /// </summary>
        [JsonProperty("touser")]
        [DisplayName("用户OpenId")]
        [Required(ErrorMessage = "缺少参数{0}")]
        public string to_user { get; set; }

        /// <summary>
        /// 用户在系统中的编号
        /// <remarks>方便参数传递，和微信端无任何联系</remarks>
        /// </summary>
        [JsonIgnore]
        [DisplayName("用户编号")]
        public int to_user_id { get; set; }

        /// <summary>
        /// 发送人在系统中的编号
        /// <remarks>方便参数传递，和微信端无任何联系</remarks>
        /// </summary>
        [JsonIgnore]
        [DisplayName("发送人编号")]
        public int from_user_id { get; set; }

        /// <summary>
        /// 消息模板编号
        /// </summary>
        [JsonProperty("template_id")]
        [DisplayName("消息模板编号")]
        [Required(ErrorMessage = "缺少参数{0}")]
        public string template_id { get; set; }

        /// <summary>
        /// 模板跳转链接
        /// </summary>
        [JsonProperty("url")]
        [DisplayName("模板跳转链接")]
        public string url { get; set; }

        /// <summary>
        /// 模板字体颜色
        /// <remarks>默认为黑色</remarks>
        /// </summary>
        [JsonProperty("color")]
        [DisplayName("模板字体颜色")]
        public string color { get; set; }

        /// <summary>
        /// 消息参数
        /// </summary>
        [JsonProperty("data")]
        [DisplayName("消息参数")]
        [Required(ErrorMessage = "缺少参数{0}")]
        public Dictionary<string, SendTemplateDataParameter> data { get; set; }

        private string _postType = "1";
        [JsonProperty("post_type")]
        public string PostType
        {
            get { return _postType; }
            set { _postType = value; }
        }

        /// <summary>
        /// 消息参数
        /// </summary>
        [Serializable]
        public class SendTemplateDataParameter
        {
            public SendTemplateDataParameter(string value)
            {
                this.value = value;
                this.color = "#173177";
            }

            public SendTemplateDataParameter(string value, string color)
            {
                this.value = value;
                this.color = color;
            }

            /// <summary>
            /// 消息参数的值
            /// </summary>
            public string value { get; set; }

            /// <summary>
            /// 消息参数值的颜色
            /// </summary>
            public string color { get; set; }
        }
    }
}