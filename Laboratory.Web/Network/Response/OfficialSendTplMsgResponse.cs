using System;

namespace Laboratory.Web.Network.Response
{
    [Serializable]
    public class OfficialSendTplMsgResponse : BaseResponse
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public string msgid { get; set; }
    }
}