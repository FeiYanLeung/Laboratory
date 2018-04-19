using Laboratory.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Laboratory.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class BridgeAuthorizationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 帮帮认证
        /// </summary>
        private const string ACCESS_TOKEN = "access_token";
        /// <summary>
        /// 应用程序认证
        /// </summary>
        private const string PASSPORT_TOKEN = "passport_token";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var token = this.GetBridgeToken(actionContext.ActionArguments, actionContext.Request.Method);

            base.OnActionExecuting(actionContext);
        }

        private BridgeToken GetBridgeToken(Dictionary<string, object> actionArguments, HttpMethod type)
        {
            var bridgeToken = new BridgeToken();

            if (type == HttpMethod.Post)
            {

                var parameter = actionArguments.Values.First() as TestParameter;    //

                foreach (var value in actionArguments.Values)
                {
                    #region 获取access_token

                    var _access_token_property = value.GetType().GetProperty(ACCESS_TOKEN);
                    if (_access_token_property == null)
                    {
                        bridgeToken.access_token = GetBridgeToken(actionArguments, HttpMethod.Get).access_token;
                    }
                    else
                    {
                        bridgeToken.access_token = _access_token_property.GetValue(value).ToString();
                    }

                    #endregion

                    #region 获取passport_token

                    var _passport_token_property = value.GetType().GetProperty(PASSPORT_TOKEN);
                    if (_passport_token_property == null)
                    {
                        bridgeToken.passport_token = GetBridgeToken(actionArguments, HttpMethod.Get).passport_token;
                    }
                    else
                    {
                        bridgeToken.passport_token = _passport_token_property.GetValue(value).ToString();
                    }

                    #endregion
                }
            }
            else if (type == HttpMethod.Get)
            {
                if (!actionArguments.ContainsKey(ACCESS_TOKEN))
                {
                    throw new Exception("未附带token!");
                }

                if (actionArguments[ACCESS_TOKEN] != null)
                {
                    bridgeToken.access_token = actionArguments[ACCESS_TOKEN].ToString();
                }
                else
                {
                    throw new Exception("token不能为空!");
                }
            }
            else
            {
                throw new Exception("暂未开放其它访问方式!");
            }

            return bridgeToken;
        }
    }
}