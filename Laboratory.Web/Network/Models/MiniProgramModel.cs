using System;

namespace Laboratory.Web.Network.Models
{
    /// <summary>
    /// 小程序
    /// </summary>
    [Serializable]
    public class MiniProgramModel
    {
        /// <summary>
        /// 所需跳转到的小程序appid（该小程序appid必须与发模板消息的公众号是绑定关联关系）
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 所需跳转到小程序的具体页面路径，支持带参数,（示例index?foo=bar）
        /// </summary>
        public string page_path { get; set; }
    }
}