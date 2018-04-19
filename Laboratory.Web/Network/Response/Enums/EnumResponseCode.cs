using System;

namespace Laboratory.Web.Network.Response
{
    [Serializable]
    public enum EnumResponseCode
    {
        /// <summary>
        /// 系统繁忙，此时请开发者稍候再试
        /// </summary>
        service_unavailable = -1,

        /// <summary>
        /// 请求成功
        /// </summary>
        success = 0,

        /// <summary>
        /// app_secret错误或者app_secret不属于这个公众号，请开发者确认app_secret的正确性
        /// </summary>
        invalid_app_secret = 40001,

        /// <summary>
        /// app_id无效
        /// </summary>
        invalid_app_id = 40013,

        /// <summary>
        /// 请确保grant_type字段值为client_credential
        /// </summary>
        invalid_grant_type = 40002,

        /// <summary>
        /// 调用接口的IP地址不在白名单中，请在接口IP白名单中进行设置
        /// </summary>
        invalid_ip = 40164
    }
}