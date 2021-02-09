using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace OrderingSystem.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            SetDateTimeFormat(config);
        }

        /// <summary>
        /// 设置api返回日期的json格式
        /// </summary>
        /// <param name="config">The configuration.</param>
        private static void SetDateTimeFormat(HttpConfiguration config)
        {
            // Convert all dates to UTC
            var json = config.Formatters.JsonFormatter;

            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            json.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
        }
    }
}
