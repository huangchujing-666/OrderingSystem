
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.IService;
using System.Web.Mvc;

namespace OrderingSystem.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly ISysAccountService _sysAccountService; 
        public LoginController(ISysAccountService sysAccountService)
        {
            _sysAccountService = sysAccountService; 
        }

        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string account = "", string password = "")
        {
            var info = _sysAccountService.Login(account, MD5Util.GetMD5_32(password));
            if (info == null)
            {
                //无此账号信息
                return Json(new { Status = -1 }, JsonRequestBehavior.AllowGet);
            }
            if (info.Status == 0)
            {
                //该账号被禁用
                return Json(new { Status = -2 }, JsonRequestBehavior.AllowGet);
            }
            var imgInfo = info.BaseImage ?? new Domain.Model.BaseImage(); 
            //缓存用户信息
            SessionHelper.Add(LoginerConst.ACCOUNT_ID, info.SysAccountId.ToString());
            SessionHelper.Add(LoginerConst.ACCOUNT, info.Account);
            SessionHelper.Add(LoginerConst.NICKNAME, info.NickName);
            SessionHelper.Add(LoginerConst.ACCOUNT_IMG, imgInfo.Source + imgInfo.Path);
            SessionHelper.Add(LoginerConst.ROLE_ID, info.SysRoleId.ToString());
            SessionHelper.Add(LoginerConst.BUSINESS_ID, info.BusinessInfoId.ToString());
            return Json(new { Status = 1 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            Loginer.DelAccountCache();
            return Redirect("/Login/Login");
        }
    }
}