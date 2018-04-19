using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory.WebApi.Models.Response
{
    public class SendTemplateMessageResponse
    {        /// <summary>
        /// 响应码
        /// </summary>
        public virtual int errcode { get; set; }

        /// <summary>
        /// 响应详情
        /// </summary>
        public virtual string errmsg { get; set; }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string msgid { get; set; }
    }
}