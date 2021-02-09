using LOT.WX.Pay;
using OrderingSystem.IService;
using OrderingSystem.IService.ResponseModel;
using OrderingSystem.WX.Pay.Mos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Core.Utils;
using OSS.Common.Encrypt;

namespace OrderingSystem.Service
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WXPayService : IWXPayService
    {
        /// <summary>
        /// 实例化微信配置
        /// </summary>
        private static WxPayTradeApi m_Api = new WxPayTradeApi();
        /// <summary>
        /// 微信统一下单
        /// </summary>
        /// <param name="reult"></param>
        public async System.Threading.Tasks.Task<WxAddPayUniOrderResp> WxDowloadOrder(PostOrder reult)
        {
            // var total_fee = 100;//Convert.ToInt32(reult.TotalMoney * 100);
            var order = new WxAddPayUniOrderReq();
            order.body = "H5下单下单支付";//string.IsNullOrWhiteSpace(reult.Subject) == true ? "一生时光下单支付" : reult.Subject;
            order.spbill_create_ip = reult.spbill_create_ip;
            order.out_trade_no = reult.order_no;// "2012121215"; //reult.OrderNo;
            order.trade_type = "JSAPI";
            order.openid = reult.open_Id;
            order.total_fee = Convert.ToInt32(reult.order_real_price * 100);//分
            var model = m_Api.AddUniOrderAsync(order);
            return await model;
        }

        /// <summary>
        /// 测试下单
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<WxAddPayUniOrderResp> WxDowloadOrder2(string ip, string openId)
        {
            var total_fee = Convert.ToInt32(1);
            var order = new WxAddPayUniOrderReq();
            order.body = "H5下单下单支付";
            order.spbill_create_ip = ip;
            order.out_trade_no = RandomHelper.GetOrderNumber();
            order.trade_type = "JSAPI";
            order.total_fee = total_fee;
            order.openid = openId == "" ? "oaU5is1U7BLlP8_zgfhkRaD6igu0" : openId;
            var model = m_Api.AddUniOrderAsync(order);
            return await model;
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string GetSign(WxAddPayUniOrderResp res)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string GenerateTimeStamp = Convert.ToInt64(ts.TotalSeconds).ToString();

            var list = new SortedDictionary<string, object>();
            list.Add("appId", res.appid);
            list.Add("timeStamp", GenerateTimeStamp);
            list.Add("nonceStr", res.nonce_str);
            list.Add("package", "prepay_id=" + res.prepay_id);
            list.Add("signType", "MD5");
            string encStr = string.Join("&",
                list.Select(
                    k =>
                    {
                        var str = k.Value?.ToString();
                        return string.IsNullOrEmpty(str)
                            ? string.Empty
                            : string.Concat(k.Key, "=", str);
                    }));
            return Md5.EncryptHexString(string.Concat(encStr, "&key=", m_Api.ApiConfig.Key)).ToUpper();
        }

    }
}
