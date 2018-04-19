using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratory.WebApi.Models
{
    public class ResultJson
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class ResultJson<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }

    }
}