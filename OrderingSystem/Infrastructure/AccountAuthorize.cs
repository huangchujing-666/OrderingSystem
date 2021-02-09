using OrderingSystem.Admin.Common;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.IService; 
using System;
using System.Web.Mvc;

namespace OrderingSystem.Admin.Infrastructure
{
    /// <summary>
    /// 账号验证
    /// </summary>
    public class AccountAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            int accountId = Loginer.AccountId;
            if (accountId == 0)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }

            base.OnActionExecuting(filterContext);
        }
         
    }
}