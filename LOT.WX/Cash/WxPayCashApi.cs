using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderingSystem.WX.Cash.Mos;
using OrderingSystem.WX.SysTools;
using OrderingSystem.Http.Mos;

namespace OrderingSystem.WX.Cash
{
    public class WxPayCashApi : WxPayBaseApi
    {
        public WxPayCashApi(WxPayCenterConfig config) : base(config)
        {
        }

        /// <summary>
        ///  企业付款接口
        /// </summary>
        /// <param name="cashReq"></param>
        /// <returns></returns>
        public async Task<WxPayTransferCashResp> TransferCashAsync(WxPayTransferCashReq cashReq)
        {
            var addressUrl = string.Concat(m_ApiUrl, "/mmpaymkttransfers/promotion/transfers");
            var dics = cashReq.GetDics();

            dics.Add("mch_appid", ApiConfig.AppId);
            dics.Add("mch_id", ApiConfig.MchId);

            CompleteDicSign(dics);

            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = addressUrl;
            req.CustomBody = dics.ProduceXml();

            return await RestCommonAsync<WxPayTransferCashResp>(req, null, GetCertHttpClient());
        }


        /// <summary>
        /// 获取企业付款信息
        /// </summary>
        /// <param name="partner_trade_no"></param>
        /// <returns></returns>
        public async Task<WxPayGetTransferCashResp> GetTransferCashAsync(string partner_trade_no)
        {
            var urlStr = string.Concat(m_ApiUrl, "/mmpaymkttransfers/gettransferinfo");

            var dics = new SortedDictionary<string, object>();
            dics["nonce_str"] = Guid.NewGuid().ToString().Replace("-", "");
            dics["partner_trade_no"] = partner_trade_no;

            return await PostApiAsync<WxPayGetTransferCashResp>(urlStr, dics, null, GetCertHttpClient());
        }
    }
}
