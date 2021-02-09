using Newtonsoft.Json;
using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OrderingSystem.Api.App_Start
{
    public class ApiSecurityFilterAttribute : ActionFilterAttribute
    {
        private readonly ISysAccountService _sysAccountService = EngineContext.Current.Resolve<ISysAccountService>();
        /// <summary>
        /// 重写OnActionExecuting 验证tokenStr
        /// </summary>
        /// <param name="actionContext">HttpActionContext</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            HttpResponseMessage response = actionContext.Request.CreateResponse(HttpStatusCode.OK);
            string SysBusinessAccountId = String.Empty, Token_Str = string.Empty, jsonStr = string.Empty;//, nonce = string.Empty, signature = string.Empty;
            var result = new ResponseModel<string>();
            result.data = "";
            result.error_code = Result.SUCCESS;
            result.message = "";
            if (request.Headers.Contains("SysBusinessAccountId") && request.Headers.Contains("TokenStr"))
            {
                SysBusinessAccountId = HttpUtility.UrlDecode(request.Headers.GetValues("SysBusinessAccountId").FirstOrDefault());
                Token_Str = HttpUtility.UrlDecode(request.Headers.GetValues("TokenStr").FirstOrDefault());

                if (string.IsNullOrWhiteSpace(SysBusinessAccountId) || int.Parse(SysBusinessAccountId) <= 0)
                {
                    result.error_code = Result.ERROR;
                    result.message = "2";
                    jsonStr = JsonConvert.SerializeObject(result);
                    response.Content = new StringContent(jsonStr, Encoding.UTF8);
                    actionContext.Response = response;
                    base.OnActionExecuting(actionContext);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Token_Str))
                {
                    result.error_code = Result.ERROR;
                    result.message = "3";
                    jsonStr = JsonConvert.SerializeObject(result);
                    response.Content = new StringContent(jsonStr, Encoding.UTF8);
                    actionContext.Response = response;
                    base.OnActionExecuting(actionContext);
                    return;
                }
                else if (_sysAccountService.GetAccountByToken(Token_Str) == null)//token错误
                {
                    result.error_code = Result.ERROR;
                    result.message = "5";
                    jsonStr = JsonConvert.SerializeObject(result);
                    response.Content = new StringContent(jsonStr, Encoding.UTF8);
                    actionContext.Response = response;
                    base.OnActionExecuting(actionContext);
                    return;

                }
                else
                {
                    var sysAccount = _sysAccountService.GetAccountByToken(Token_Str);
                    if (int.Parse(SysBusinessAccountId) == sysAccount.BusinessInfoId)
                    {
                        if (sysAccount.LoginTime.AddMonths(1)<=System.DateTime.Now)//登录过期
                        {
                            sysAccount.Token = "";
                            _sysAccountService.Update(sysAccount);
                            result.error_code = Result.ERROR;
                            result.message = "6";
                            jsonStr = JsonConvert.SerializeObject(result);
                            //jsonStr = JsonConvert.SerializeObject(new { Success = false, Msg = "4", Data = new { } });
                            response.Content = new StringContent(jsonStr, Encoding.UTF8);
                            actionContext.Response = response;
                            base.OnActionExecuting(actionContext);
                            return;
                        }
                        else
                        {
                            base.OnActionExecuting(actionContext);
                        }
                    }
                    else//商家id错误
                    {
                        result.error_code = Result.ERROR;
                        result.message = "4";
                        jsonStr = JsonConvert.SerializeObject(result);
                        response.Content = new StringContent(jsonStr, Encoding.UTF8);
                        actionContext.Response = response;
                        base.OnActionExecuting(actionContext);
                        return;
                    }
                    //Token token = (Token)HttpRuntime.Cache.Get(SysBusinessAccountId);
                    //if (!token.Token_Str.Equals(Token_Str))
                    //{
                    //    jsonStr = JsonConvert.SerializeObject(new { Success = false, Msg = "5", Data = new { } });
                    //    response.Content = new StringContent(jsonStr, Encoding.UTF8);
                    //    actionContext.Response = response;
                    //    base.OnActionExecuting(actionContext);
                    //    return;
                    //}
                    //else if (token.Token_Str.Equals(Token_Str) && token.Erpert_Time < System.DateTime.Now)
                    //{
                    //    HttpRuntime.Cache.Remove(SysBusinessAccountId);
                    //    jsonStr = JsonConvert.SerializeObject(new { Success = false, Msg = "6", Data = new { } });
                    //    response.Content = new StringContent(jsonStr, Encoding.UTF8);
                    //    actionContext.Response = response;
                    //    base.OnActionExecuting(actionContext);
                    //    return;
                    //}
                    //else
                    //{
                    //    base.OnActionExecuting(actionContext);
                    //}
                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "1";
                jsonStr = JsonConvert.SerializeObject(result);
                response.Content = new StringContent(jsonStr, Encoding.UTF8);
                actionContext.Response = response;
                base.OnActionExecuting(actionContext);
                return;
            }
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}