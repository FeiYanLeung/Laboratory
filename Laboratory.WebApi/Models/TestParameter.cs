using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.WebApi.Models
{
    [Serializable]
    public class TestParameter
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        [DisplayName("访问令牌")]
        [Required(ErrorMessage = "缺少参数{0}")]
        public string access_token { get; set; }

        public string passport_token { get; set; }
    }
}