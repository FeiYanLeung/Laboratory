using System;

namespace Laboratory.WebApi.Models
{
    [Serializable]
    public class BridgeToken
    {
        public string access_token { get; set; }
        public string passport_token { get; set; }
    }
}