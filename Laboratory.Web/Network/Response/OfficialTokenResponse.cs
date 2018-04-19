using System;

namespace Laboratory.Web.Network.Response
{
    /// <summary>
    /// 公众号 Token
    /// </summary>
    [Serializable]
    public class OfficialTokenResponse : BaseResponse
    {
        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int expires_in { get; set; }
    }
}