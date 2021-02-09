using OrderingSystem.Api.App_Start;
using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    public class SysAccountApiController : ApiController
    {
        private readonly ISysAccountService _sysAccountService = EngineContext.Current.Resolve<ISysAccountService>();
        private readonly string token_key = "lead_ordering_system";

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="bcloginDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<BCSysAccountDTO> Login(BCLoginDTO bcloginDTO)
        {
            var result = new ResponseModel<BCSysAccountDTO>();
            var data = new BCSysAccountDTO();
            result.error_code = Result.SUCCESS;
            result.message = "";
            var info = _sysAccountService.Login(bcloginDTO.account, bcloginDTO.password);//.SysBusinessAccount(sysBusinessAccountLoginDto.account, sysBusinessAccountLoginDto.password);
            if (info == null || info.SysAccountId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "您输入的帐号或密码错误，请重新输入";
                result.data = data;
                return result;
            }
            else
            {
                DateTime time = System.DateTime.Now;
                if (info.LoginTime.AddMonths(1) < System.DateTime.Now||string.IsNullOrWhiteSpace(info.Token))//过期
                {
                    string tokenstr = MD5Util.GetMD5_32(info.PassWord + info.SysAccountId + time.ToString("yyyy:MM:dd HH:mm:ss") + token_key);
                    data.last_loin_time = info.LoginTime.ToString("yyyy:MM:dd HH:mm:ss");
                    data.nick_name = info.NickName;
                    data.path = info.BaseImage == null ? "" : info.BaseImage.Source + info.BaseImage.Path;
                    data.phone_no = info.MobilePhone;
                    data.sys_business_account_id = info.BusinessInfoId;
                    data.token_str = tokenstr;
                    data.account = info.Account;
                    info.Token = tokenstr;
                    info.LoginTime = time;
                    _sysAccountService.Update(info);
                    result.data = data;
                }
                else
                {
                    data.last_loin_time = info.LoginTime.ToString("yyyy:MM:dd HH:mm:ss");
                    data.nick_name = info.NickName;
                    data.path = info.BaseImage == null ? "" : info.BaseImage.Source + info.BaseImage.Path;
                    data.phone_no = info.MobilePhone;
                    data.sys_business_account_id = info.BusinessInfoId;
                    data.token_str = info.Token;
                    data.account = info.Account;
                    info.LoginTime = System.DateTime.Now;
                    _sysAccountService.Update(info);
                    result.data = data;
                }
            }
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sysBusinessAccountUpdPwdDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiSecurityFilter]
        public ResponseModel<string> UpdatePassword(BCUpdPwdDTO bcupdPwdDTO)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            result.message = "";
            if (bcupdPwdDTO.account_id <= 0 || string.IsNullOrWhiteSpace(bcupdPwdDTO.account) || string.IsNullOrWhiteSpace(bcupdPwdDTO.oldPassword) || string.IsNullOrWhiteSpace(bcupdPwdDTO.newPassword))
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                result.data = "";
            }
            else
            {
                string msg = string.Empty;
                if (!_sysAccountService.UpdatePassWord(bcupdPwdDTO.account_id, bcupdPwdDTO.account, bcupdPwdDTO.oldPassword, bcupdPwdDTO.newPassword, out msg))
                {
                    result.message = msg;
                    result.data = "";
                    result.error_code = Result.ERROR;
                }
            }
            return result;
        }

        /// <summary>
        /// 登出接口
        /// </summary>
        /// <param name="Business_Id"></param>
        /// <param name="Token_Str"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiSecurityFilter]
        public ResponseModel<string> LoginOut(BCGetSysAccount bcgetSysAccount)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            result.message = "";
            result.data = "";
            var sysAccount=  _sysAccountService.GetAccountByToken(bcgetSysAccount.Token_Str);
            if (sysAccount==null)
            {
                result.error_code = Result.ERROR;
                result.message = "token不存在";
            }
            else if(sysAccount.BusinessInfoId== bcgetSysAccount.Business_Id)
            {
                sysAccount.Token = "";
                _sysAccountService.Update(sysAccount);
                result.message = "登出成功";
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "商家id有误";
            }
            return result;
        }
        [HttpPost]
        [ApiSecurityFilter]
        /// <summary>
        /// 获取商家账户信息
        /// </summary>
        /// <param name="Business_Id"></param>
        /// <param name="Token_Str"></param>
        /// <returns></returns>
        public ResponseModel<BCSysAccountDTO> GetSysAccountById(BCGetSysAccount bcgetSysAccount)
        {
            var result = new ResponseModel<BCSysAccountDTO>();
            var data = new BCSysAccountDTO();
            result.error_code = Result.SUCCESS;
          
            if (bcgetSysAccount.Business_Id <= 0||string.IsNullOrWhiteSpace(bcgetSysAccount.Token_Str))
            {
                result.error_code = Result.ERROR;
                result.message = "参数不为空";
                result.data = data;
            }
            else
            {
                var info = _sysAccountService.GetAccountByToken(bcgetSysAccount.Token_Str);
                if (info==null|| info.BusinessInfoId!= bcgetSysAccount.Business_Id)
                {
                    result.error_code = Result.ERROR;
                    result.message = "商家id或token错误";
                }
                else
                {
                    data.last_loin_time = info.LoginTime.ToString("yyyy:MM:dd HH:mm:ss");
                    data.nick_name = info.NickName;
                    data.path = info.BaseImage == null ? "" : info.BaseImage.Source + info.BaseImage.Path;
                    data.phone_no = info.MobilePhone;
                    data.sys_business_account_id = info.BusinessInfoId;
                    data.account = info.Account;
                }
            }
            result.data = data;
            return result;
        }
    }
}
