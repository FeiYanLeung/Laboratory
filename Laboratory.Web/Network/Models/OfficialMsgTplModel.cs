using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.Web.Network.Models
{
    /// <summary>
    /// 消息发送内容模板
    /// </summary>
    [Serializable]
    public class OfficialMsgTplModel
    {
        public OfficialMsgTplModel()
        {
            this.data = new Dictionary<string, OfficialMsgTplDataItemModel>();
        }

        /// <summary>
        /// 接收者openid
        /// </summary>
        [DisplayName("接收者openid")]
        [Required(ErrorMessage = "请填写{0}")]
        public string touser { get; set; }

        /// <summary>
        /// 	模板Id
        /// </summary>
        [DisplayName("模板Id")]
        [Required(ErrorMessage = "请填写{0}")]
        public string template_id { get; set; }

        /// <summary>
        /// 模板跳转链接
        /// Required:false
        /// </summary>
        [DisplayName("模板跳转链接")]
        public string url { get; set; }

        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// Required:false
        /// </summary>
        [DisplayName("小程序数据")]
        public MiniProgramModel miniprogram { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        [DisplayName("模板数据")]
        [Required(ErrorMessage = "请填写{0}")]
        public Dictionary<string, OfficialMsgTplDataItemModel> data { get; set; }
    }

    /// <summary>
    /// 模板数据项
    /// </summary>
    [Serializable]
    public class OfficialMsgTplDataItemModel
    {
        /// <summary>
        /// 模板项的值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 模板内容字体颜色，默认为黑色
        /// </summary>
        public string color { get; set; }
    }
}