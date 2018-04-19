using System;

namespace Laboratory.Web.Network.Models
{
    /// <summary>
    /// 行业模型类
    /// </summary>
    [Serializable]
    public class OfficialIndustryModel
    {
        /// <summary>
        /// 主行业
        /// </summary>
        public string first_class { get; set; }
        /// <summary>
        /// 副行业
        /// </summary>
        public string second_class { get; set; }
    }
}