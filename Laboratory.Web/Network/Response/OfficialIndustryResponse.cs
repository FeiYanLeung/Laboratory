using Laboratory.Web.Network.Models;
using System;

namespace Laboratory.Web.Network.Response
{
    /// <summary>
    /// 行业信息
    /// </summary>
    [Serializable]
    public class OfficialIndustryResponse : BaseResponse
    {
        /// <summary>
        /// 主营行业
        /// </summary>
        public OfficialIndustryModel primary_industry { get; set; }

        /// <summary>
        /// 副营行业
        /// </summary>
        public OfficialIndustryModel secondary_industry { get; set; }
    }
}