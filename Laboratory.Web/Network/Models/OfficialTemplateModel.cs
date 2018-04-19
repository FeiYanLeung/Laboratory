using System;

namespace Laboratory.Web.Network.Models
{
    /// <summary>
    /// 模板列表
    /// </summary>
    [Serializable]
    public class OfficialTemplateModel
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 模板标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 模板所属行业的一级行业
        /// </summary>
        public string primary_industry { get; set; }

        /// <summary>
        /// 模板所属行业的二级行业
        /// </summary>
        public string deputy_industry { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 模板示例
        /// </summary>
        public string example { get; set; }
    }
}