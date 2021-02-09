using OrderingSystem.IService.ResponseModel;
using OrderingSystem.WX.Pay.Mos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.IService
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public interface IWXPayService
    {
        /// <summary>
        /// 微信统一下单
        /// </summary>
        /// <param name="reult"></param>
        System.Threading.Tasks.Task<WxAddPayUniOrderResp> WxDowloadOrder(PostOrder reult);

        /// <summary>
        /// 测试下单
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        System.Threading.Tasks.Task<WxAddPayUniOrderResp> WxDowloadOrder2(string ip, string openId);

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        string GetSign(WxAddPayUniOrderResp res);
    }
}
