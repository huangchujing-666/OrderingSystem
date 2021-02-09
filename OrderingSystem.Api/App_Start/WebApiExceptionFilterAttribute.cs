using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using OrderingSystem.Api.Models;

namespace OrderingSystem.Api.App_Start
{
    /// <summary>
    /// 异常处理类
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 重写基类的异常处理方法
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpResponseMessage response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK);
            string jsonStr = JsonConvert.SerializeObject(new { error_code = Result.ERROR, message = actionExecutedContext.Exception.Message, data = "" });
            response.Content = new StringContent(jsonStr, Encoding.UTF8);
            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}