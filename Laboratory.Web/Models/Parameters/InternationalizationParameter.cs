using Laboratory.Resources;
using Laboratory.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Laboratory.Web.Models.Parameters
{
    public class InternationalizationParameter
    {
        public InternationalizationParameter() { }

        /// <summary>
        /// 国家或地区
        /// </summary>
        [Display(Name = nameof(Resource.Country), ResourceType = typeof(Resource))]
        public string Country { get; set; }

        /// <summary>
        /// 国家或地区
        /// </summary>
        [LocalizedDisplayName(nameof(Resource.Country))]
        public string LocalizedCountry { get; set; }
    }
}