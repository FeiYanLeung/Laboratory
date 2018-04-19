using Laboratory.Core;
using Laboratory.Web.Network.Models;
using Laboratory.Web.Network.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Laboratory.Web.Helpers
{
    public class WeChatOfficialHelper
    {
        /// <summary>
        /// 锁
        /// </summary>
        private static object _locker = new object();

        #region 公众号配置

        /// <summary>
        /// appid 
        /// </summary>
        private const string official_app_id = "wx4f49aa6d082705cf";

        /// <summary>
        /// secret 
        /// </summary>
        private const string official_app_secret = "676a3366cc47a9c35c2db940eaeb1c37";

        /// <summary>
        /// token地址获取[GET]
        /// <remarks>
        /// 公众号的全局唯一接口调用凭据
        /// 长度至少保留512个字节
        /// 有效期2小时，重复获取后上一次的token将失效
        /// 调用前需要将服务器ip设置到白名单中，否则可能调用失败</remarks>
        /// </summary>
        private const string official_token = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        /// <summary>
        /// 设置公众号行业[POST]
        /// <remarks>
        /// 每月可修改行业1次，帐号仅可使用所属行业中相关的模板
        /// 行业编号来源：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1433751277
        /// industry_id1:主行业编号
        /// industry_id1:副行业编号
        /// </remarks>
        /// </summary>
        private const string official_set_industry = "https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token={0}";

        /// <summary>
        /// 获取公众号行业[GET]
        /// </summary>
        private const string official_get_industry = "https://api.weixin.qq.com/cgi-bin/template/get_industry?access_token={0}";

        /// <summary>
        /// 添加模板到公众号[POST]
        /// </summary>
        private const string official_add_tpl = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}";

        /// <summary>
        /// 获取已添加至帐号下所有模板列表[GET]
        /// </summary>
        private const string official_get_all_private_tpl = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template?access_token={0}";

        /// <summary>
        /// 删除帐号下的模板[POST]
        /// </summary>
        private const string official_del_tpl = "https://api.weixin.qq.com/cgi-bin/template/del_private_template?access_token={0}";

        /// <summary>
        /// 发送模板消息[POST]
        /// </summary>
        private const string official_tpl_msg_send = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";

        #endregion

        /// <summary>
        /// 获取token[GET]
        /// </summary>
        /// <returns></returns>
        private OfficialTokenResponse token()
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;

                    var getTokenUrl = String.Format(official_token, official_app_id, official_app_secret);
                    string srcResponse = client.DownloadString(getTokenUrl);

                    return srcResponse.ToModel<OfficialTokenResponse>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 设置公众号行业[POST]
        /// </summary>
        /// <param name="access_token">公众号全局访问token</param>
        /// <param name="industry_id1">主行业编号</param>
        /// <param name="industry_id2">副行业编号</param>
        private void setIndustry(string access_token, string industry_id1, string industry_id2)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var formBodyPair = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("industry_id1", industry_id1),
                        new KeyValuePair<string, string>("industry_id2", industry_id2)
                    }
                    .Select(q => String.Format("{0}={1}", q.Key, HttpUtility.UrlEncode(q.Value)));

                    var requestUrl = String.Format(official_set_industry, access_token);
                    var response = client.UploadData(requestUrl, "POST", Encoding.UTF8.GetBytes(String.Join("&", formBodyPair)));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取公众号行业[GET]
        /// </summary>
        /// <param name="access_token">公众号全局访问token</param>
        /// <returns></returns>
        private OfficialIndustryResponse getIndustry(string access_token)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;

                    var getIndustryUrl = String.Format(official_get_industry, access_token);
                    var srcResponse = client.DownloadString(getIndustryUrl);

                    return srcResponse.ToModel<OfficialIndustryResponse>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取公众号行业[GET]
        /// </summary>
        /// <param name="access_token">公众号全局访问token</param>
        /// <param name="template_id_short">模板库中模板的编号</param>
        /// <returns></returns>
        private OfficialAddTemplateResponse addTemplate(string access_token, string template_id_short)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var formBodyPair = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("template_id_short", template_id_short)
                    }
                    .Select(q => String.Format("{0}={1}", q.Key, HttpUtility.UrlEncode(q.Value)));

                    var requestUrl = String.Format(official_set_industry, access_token);
                    var response = client.UploadData(requestUrl, "POST", Encoding.UTF8.GetBytes(String.Join("&", formBodyPair)));
                    string srcResponse = Encoding.UTF8.GetString(response);

                    return srcResponse.ToModel<OfficialAddTemplateResponse>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取已添加至帐号下所有模板列表
        /// </summary>
        /// <param name="access_token">公众号全局访问token</param>
        private OfficialTempateResponse getTemplates(string access_token)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;

                    var getTplUrl = String.Format(official_get_all_private_tpl, access_token);
                    var srcResponse = client.DownloadString(getTplUrl);

                    return srcResponse.ToModel<OfficialTempateResponse>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除帐号下的模板
        /// </summary>
        /// <param name="access_token">公众号全局访问token</param>
        /// <param name="template_id">模板编号</param>
        /// <returns></returns>
        private BaseResponse delTemplate(string access_token, string template_id)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var formBodyPair = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("template_id", template_id)
                    }
                    .Select(q => String.Format("{0}={1}", q.Key, HttpUtility.UrlEncode(q.Value)));

                    var requestUrl = String.Format(official_del_tpl, access_token);
                    var response = client.UploadData(requestUrl, "POST", Encoding.UTF8.GetBytes(String.Join("&", formBodyPair)));
                    string srcResponse = Encoding.UTF8.GetString(response);

                    return srcResponse.ToModel<OfficialAddTemplateResponse>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 发送模板消息
        /// </summary>
        private OfficialSendTplMsgResponse sendTplMsg(string access_token, OfficialMsgTplModel template)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                   
                    var requestUrl = String.Format(official_tpl_msg_send, access_token);
                    var response = client.UploadData(requestUrl, "POST", Encoding.UTF8.GetBytes(template.ToJson()));
                    string srcResponse = Encoding.UTF8.GetString(response);

                    return srcResponse.ToModel<OfficialSendTplMsgResponse>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}