using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Common.SMS;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    /// <summary>
    /// 个人中心接口
    /// </summary>
    public class UserApiController : ApiController
    {
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        private readonly ISmsLogService _smsLogService = EngineContext.Current.Resolve<ISmsLogService>();

        /// <summary>
        /// 根据用户Id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel<UserDTO> GetUserInfoById(int UserId)
        {
            var result = new ResponseModel<UserDTO>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            //参数验证
            if (UserId > 0)
            {
                var getResult = _userService.GetById(UserId);
                if (getResult != null)
                {
                    result.data = new UserDTO()
                    {
                        id = getResult.UserId,
                        mobile = getResult.PhoneNo,
                        name = getResult.NickName,
                        user_img_id = getResult.BaseImageId.ToString(),
                        openid = getResult.OpenId,
                        user_img_path = getResult.BaseImage == null ? "" : getResult.BaseImage.Source + getResult.BaseImage.Path
                    };
                    result.total_count = 1;
                }
            }
            else
            {
                result.message = "参数不合法";
                result.error_code = Result.ERROR;
            }
            return result;
        }


        /// <summary>
        /// 绑定手机，发送验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel SendBindMobileCode(string Mobile, string OpenId)
        {
            var result = new ResponseModel();
            object obj = new object();
            result.error_code = Result.SUCCESS;
            if (CheckInputHelper.RegexPhone(Mobile) && !string.IsNullOrWhiteSpace(OpenId))
            {
                var user = _userService.GetUserByPhoneOrOpenId(Mobile, OpenId);
                if (user == null)
                {
                    //发送验证码
                    lock (obj)
                    {
                        string code = Assistant.GetRandomNumber(6);
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("code", code);
                        dic.Add("product", "H5点餐系统");
                        var a = SendSMSCommon.SendSMSsingleAsync(SmsTemplate.SmsRegister, Mobile, dic);
                        _smsLogService.Insert(new SmsLog()
                        {
                            Code = code,
                            CreateTime = System.DateTime.Now,
                            Module = (int)EnumHelp.SmsLogModuleEnum.前端,
                            Phone = Mobile,
                            Remark = "手机绑定验证码"
                        });
                    }
                }
                else
                {
                    result.message = "您已绑定手机号,请勿重复绑定";
                    result.error_code = Result.ERROR;
                }
            }
            else
            {
                result.message = "手机号码或OpenId有误";
                result.error_code = Result.ERROR;
            }
            return result;
        }

        /// <summary>
        /// 绑定手机
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> BindMobile(PostBindMobile postBindMobile)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            //参数验证
            if (!string.IsNullOrWhiteSpace(postBindMobile.Mobile) && !string.IsNullOrWhiteSpace(postBindMobile.Code) && !string.IsNullOrWhiteSpace(postBindMobile.OpenId) && CheckInputHelper.RegexPhone(postBindMobile.Mobile))
            {
                var getResult = _smsLogService.GetByPhoneNo(postBindMobile.Mobile);
                //获取验证码数据
                if (getResult != null)
                {
                    //判断验证码正误
                    if (getResult.Code == postBindMobile.Code)
                    {

                        var userByPhone = _userService.GetUserByPhoneOrOpenId(postBindMobile.Mobile, "");
                        if (userByPhone != null)//解绑
                        {
                            userByPhone.PhoneNo = "";
                            userByPhone.EditTime = System.DateTime.Now;
                            userByPhone.EditPersonId = (int)EnumHelp.SmsLogModuleEnum.前端;
                            _userService.Update(userByPhone);
                        }
                        var user = _userService.GetUserByPhoneOrOpenId("", postBindMobile.OpenId);
                        if (user != null)
                        {
                            user.PhoneNo = postBindMobile.Mobile;
                            user.EditTime = System.DateTime.Now;
                            _userService.Update(user);
                            result.data = user.UserId;

                        }
                        else
                        {
                            result.error_code = Result.ERROR;
                            result.message = "绑定失败OpenId不存在";
                        }
                    }
                    else
                    {
                        result.error_code = Result.ERROR;
                        result.message = "验证码错误";
                    }
                }
                else
                {
                    result.error_code = Result.ERROR;
                    result.message = "请先发送验证码";
                }
            }
            else
            {
                result.message = "参数无效";
                result.error_code = Result.ERROR;
            }
            return result;
        }

        /// <summary>
        /// 约饭用户身份验证
        /// </summary>
        /// <param name="userAuthorityDTO"></param>
        /// <returns></returns>

        [HttpPost]
        public ResponseModel<int> UserAuthority(UserAuthorityDTO userAuthorityDTO)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            var user = _userService.GetUserByOpenId(userAuthorityDTO.OpenId);
            if (user != null && user.UserId > 0)
            {
                user.Sex = userAuthorityDTO.Sex;
                user.UserName = userAuthorityDTO.UserName;
                user.CardNo = userAuthorityDTO.CardNo;
                user.BirthDay = userAuthorityDTO.BirthDay;
                _userService.Update(user);
                result.data = user.UserId;
            }
            else
            {
                result.data = 0;
                result.error_code = Result.ERROR;
                result.message = "OpenId不存在";
            }
            return result;
        }
    }
}
