using Laboratory.Web.Network.Models;
using System;
using System.Collections.Generic;

namespace Laboratory.Web.Network.Response
{
    /// <summary>
    /// 获取模板列表返回相应实体
    /// </summary>
    [Serializable]
    public class OfficialTempateResponse : BaseResponse
    {
        public OfficialTempateResponse()
        {
            this.template_list = new List<OfficialTemplateModel>();
        }

        /// <summary>
        /// 模板列表
        /// </summary>
        public IList<OfficialTemplateModel> template_list { get; set; }
    }

    /// <summary>
    /// 添加模板返回实体
    /// </summary>
    [Serializable]
    public class OfficialAddTemplateResponse : BaseResponse
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        public string template_id { get; set; }
    }
}