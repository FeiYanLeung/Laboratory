using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.NetworkTest
{
    public static class Network
    {
        #region Request

        private static readonly object _locker = new object();
        private static WebClient _req = null;
        public static WebClient Request
        {
            get
            {
                lock (_locker)
                {
                    if (_req == null) _req = new WebClient();
                    return _req;
                }
            }
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 设置请求头信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static WebClient AddHeader(this WebClient req, string name, string value)
        {
            req.Headers.Add(name, value);
            return req;
        }

        /// <summary>
        /// 设置请求头信息
        /// </summary>
        /// <param name="req"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static WebClient AddHeaders(this WebClient req, params NameValueCollection[] headers)
        {
            if (headers.Length > 0)
            {
                foreach (var header in headers)
                {
                    req.Headers.Add(header);
                }
            }
            return req;
        }

        /// <summary>
        /// Get请求，返回服务器响应内容
        /// </summary>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string Get(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            using (req)
            {
                var formBodyPair = pairs.Select(q => String.Format("{0}={1}", q.Key, q.Value));
                return req.DownloadString(String.Concat(address, "?", String.Join("&", formBodyPair)));
            }
        }

        /// <summary>
        /// Get请求，返回服务器响应内容
        /// </summary>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static T Get<T>(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            var srcResponse = Get(req, address, pairs);
            return JsonConvert.DeserializeObject<T>(srcResponse);
        }

        /// <summary>
        /// Get请求，返回服务器响应内容
        /// </summary>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            using (req)
            {
                var formBodyPair = pairs.Select(q => String.Format("{0}={1}", q.Key, q.Value));
                return await req.DownloadStringTaskAsync(String.Concat(address, "?", String.Join("&", formBodyPair)));
            }
        }

        /// <summary>
        /// Get请求，返回服务器响应内容
        /// </summary>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            var srcResponse = await GetAsync(req, address, pairs);
            return JsonConvert.DeserializeObject<T>(srcResponse);
        }

        /// <summary>
        /// POST请求，返回服务器响应内容
        /// </summary>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string Post(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            using (req)
            {
                if (string.IsNullOrEmpty(req.Headers.Get("Content-Type")))
                {
                    req.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                }
                var formBodyPair = pairs.Select(q => String.Format("{0}={1}", q.Key, q.Value));
                var response = req.UploadData(address, "POST", Encoding.UTF8.GetBytes(String.Join("&", formBodyPair)));
                return Encoding.UTF8.GetString(response);
            }
        }

        /// <summary>
        /// POST请求，返回处理后的服务器响应对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static T Post<T>(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            var srcResponse = Post(req, address, pairs);
            return JsonConvert.DeserializeObject<T>(srcResponse);
        }

        /// <summary>
        /// POST请求，返回服务器响应内容
        /// </summary>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            using (req)
            {
                if (string.IsNullOrEmpty(req.Headers.Get("Content-Type")))
                {
                    req.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                }
                var formBodyPair = pairs.Select(q => String.Format("{0}={1}", q.Key, q.Value));
                var response = await req.UploadDataTaskAsync(address, "POST", Encoding.UTF8.GetBytes(String.Join("&", formBodyPair)));
                return Encoding.UTF8.GetString(response);
            }
        }

        /// <summary>
        /// POST请求，返回处理后的服务器响应对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="address"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(this WebClient req, string address, params KeyValuePair<string, string>[] pairs)
        {
            var srcResponse = await req.PostAsync(address, pairs);
            return JsonConvert.DeserializeObject<T>(srcResponse);
        }

        /// <summary>
        /// 发起POST请求(WebRequest)，返回处理后的服务器响应对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webRequest">HttpWebRequest实例对象</param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static string Post(this HttpWebRequest webRequest, params KeyValuePair<string, string>[] pairs)
        {
            var formBodyPair = pairs.Select(q => String.Format("{0}={1}", q.Key, q.Value));
            var payload = Encoding.UTF8.GetBytes(String.Join("&", formBodyPair));

            // 设置提交的相关参数 
            webRequest.Method = "POST";
            webRequest.KeepAlive = false;
            webRequest.AllowAutoRedirect = true;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            webRequest.ContentLength = payload.Length;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            // 提交请求数据 
            using (var outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(payload, 0, payload.Length);
            }

            using (var response = webRequest.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        /// <summary>
        /// 发起POST请求(WebRequest)，返回处理后的服务器响应对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webRequest">HttpWebRequest实例对象</param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public static T Post<T>(this HttpWebRequest webRequest, params KeyValuePair<string, string>[] pairs)
        {
            string responseContent = webRequest.Post(pairs);
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        #endregion
    }
}
