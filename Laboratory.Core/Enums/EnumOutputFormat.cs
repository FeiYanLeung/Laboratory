using System;
using System.ComponentModel;

namespace Laboratory.Core.Enums
{
    /// <summary>
    /// 身份验证失败时的处理方式
    /// </summary>
    [Serializable]
    public enum EnumOutputFormat
    {
        /// <summary>
        /// 跳转到指定页面
        /// </summary>
        [Description("跳转到指定页面")]
        Redirect,
        /// <summary>
        /// 输出JSON
        /// </summary>
        [Description("输出JSON")]
        JSON
    }
}
