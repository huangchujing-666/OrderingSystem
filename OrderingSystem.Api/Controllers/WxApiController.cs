using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.IService;
using OSS.Common.Encrypt;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using WeChatJsSdk.SdkCore;

namespace OrderingSystem.Api.Controllers
{
    public class WxApiController : ApiController
    {
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();

        private readonly IWXPayService _wXPayService = EngineContext.Current.Resolve<IWXPayService>();
        
        private string appId = ConfigurationManager.AppSettings["WeixinAppId"];
        private string secret = ConfigurationManager.AppSettings["WeixinAppSecret"];

        /// <summary>
        /// 拼接url让前端访问获取code然后根据code访问GetUserInfo
        /// </summary>
        /// <param name="rtnUrl"></param>
        /// <returns></returns>
        public string GetAuthorizeUrl(string rtnUrl = "")
        {

            rtnUrl = System.Web.HttpUtility.UrlEncode(rtnUrl);
            //string OAuthurl = OAuthApi.GetAuthorizeUrl(appId, rtnUrl, "AuthorizeSelect", OAuthScope.snsapi_userinfo);

            //OAuthApi.GetAuthorizeUrl(appId, rtnUrl, OAuthScope.snsapi_userinfo);

            string s = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appId + "&redirect_uri=" + rtnUrl + "&response_type=code&scope=" + OAuthScope.snsapi_userinfo + "&state=BusinessInfoId:1,SeatNo:1#wechat_redirect";

            return s;
        }

        /// <summary>
        /// 根据auth_code获取用户信息
        /// </summary>
        /// <param name="auth_code"></param>
        /// <returns></returns>
        public ResponseModel<UserDTO> GetUserInfo(string auth_code = "")
        {

            var result = new ResponseModel<UserDTO>();
            result.error_code = Models.Result.SUCCESS;
            result.total_count = 0;
            if (string.IsNullOrEmpty(auth_code))
            {
                result.message = "参数不合法";
                result.error_code = Models.Result.ERROR;
                return result;
            }
            var oAuth = OAuthApi.GetAccessToken(appId, secret, auth_code);
            string openid = string.Empty;
            if (oAuth.errcode == ReturnCode.请求成功)
            {
                openid = oAuth.openid;
                var user = _userService.GetUserByOpenId(openid);
                var userInfo = OAuthApi.GetUserInfo(oAuth.access_token, openid);
                if (user != null)
                {
                    //string str = "调到这里啦user不为空";
                    //System.IO.File.WriteAllText(@"C:\Release_OderSystem\Api\txt\1.txt", str);
                    result.data = new UserDTO()
                    {
                        id = user.UserId,
                        mobile = user.PhoneNo,
                        name = user.NickName,
                        user_img_id = user.BaseImageId.ToString(),
                        user_img_path = userInfo.headimgurl,// user.BaseImage == null ? "" : user.BaseImage.Source + user.BaseImage.Path,
                        openid = openid
                    };
                    //string str1 = "调到这里啦user图片为"+ userInfo.headimgurl;
                    //System.IO.File.WriteAllText(@"C:\Release_OderSystem\Api\txt\1.txt", str1);
                    var baseImage=  _baseImageService.GetById(user.BaseImageId);
                    if (baseImage!=null)
                    {
                        //string str2 = "调到这里啦baseImage不为空" + baseImage.BaseImageId;
                        //System.IO.File.WriteAllText(@"C:\Release_OderSystem\Api\txt\1.txt", str2+ userInfo.headimgurl);
                        if (!baseImage.Path.Equals(userInfo.headimgurl))
                        {
                            //System.IO.File.WriteAllText(@"C:\Release_OderSystem\Api\txt\1.txt", "比较成功");
                            baseImage.Path = userInfo.headimgurl;
                            //baseImage.CreateTime = System.DateTime.Now;
                            _baseImageService.Update(baseImage);
                            //System.IO.File.WriteAllText(@"C:\Release_OderSystem\Api\txt\1.txt", "update完了");
                        }
                    }
                    result.total_count = 1;
                }
                else
                {
                    var baseImage = _baseImageService.Insert(new Domain.Model.BaseImage
                    {
                        Title = "微信图像",
                        CreateTime = DateTime.Now,
                        Path = userInfo.headimgurl
                    });
                    var userInsert = _userService.Insert(new Domain.Model.User
                    {
                        IsDelete = 0,
                        NickName = userInfo.nickname,
                        OpenId = userInfo.openid,
                        Status = 1,
                        CreateTime = DateTime.Now,
                        EditTime = DateTime.Now,
                        BaseImageId = baseImage.BaseImageId
                    });
                    result.data = new UserDTO()
                    {
                        id = userInsert.UserId,
                        mobile = userInsert.PhoneNo,
                        name = userInsert.NickName,
                        user_img_id = user.BaseImageId.ToString(),
                        user_img_path = userInfo.headimgurl,
                        openid = openid
                    };
                    result.total_count = 1;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<SignPackage> GetConfig(string Url = "")
        {
            
            JSSDK sdk = new JSSDK(appId, secret, false);
            SignPackage config = sdk.GetSignPackage(JsApiEnum.scanQRCode | JsApiEnum.onMenuShareQQ | JsApiEnum.onMenuShareTimeline | JsApiEnum.onMenuShareAppMessage, Url);
            var result = new ResponseModel<SignPackage>();
            result.error_code = Models.Result.SUCCESS;
            result.total_count = 1;
            result.data = config;
            return result;
        }

        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public string GetIp() {
            string ip = ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            return ip;
        }

        /// <summary>
        /// 测试支付
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async System.Threading.Tasks.Task<WXPayDto> WxZf(string OpenId="")
        {
            string ip= HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();
            var res = await _wXPayService.WxDowloadOrder2(ip, OpenId);
            WXPayDto model = new WXPayDto();
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string GenerateTimeStamp = Convert.ToInt64(ts.TotalSeconds).ToString();

            if (res != null)
            {
                model.appId = res.appid;
                model.nonce_str = res.nonce_str;
                model.prepay_id = "prepay_id=" + res.prepay_id;
                model.sign_type = "MD5";
                model.timeStamp = GenerateTimeStamp;
                model.paySign = _wXPayService.GetSign(res);
            }
            return model;
        }

    }
}
