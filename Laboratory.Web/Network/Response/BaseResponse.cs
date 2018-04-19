using System;

namespace Laboratory.Web.Network.Response
{
    /// <summary>
    /// 基础响应信息
    /// </summary>
    [Serializable]
    public class BaseResponse
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public virtual EnumResponseCode errcode { get; set; }

        /// <summary>
        /// 响应详情
        /// </summary>
        public virtual string errmsg { get; set; }
    }
}