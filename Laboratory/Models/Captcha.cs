using System;
using System.Collections.Generic;

namespace Plantform.Models
{
    public partial class Captcha
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Identifier { get; set; }
        public System.DateTime ExpireTimeOnUtc { get; set; }
        public int CaptchaDomain { get; set; }
    }
}
