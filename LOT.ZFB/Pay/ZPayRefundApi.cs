using System.Threading.Tasks;
using OrderingSystem.ZFB.Pay.Mos;

namespace OrderingSystem.ZFB.Pay
{
    /// <summary>
    ///  退款接口
    /// </summary>
    public class ZPayRefundApi:ZPayBaseApi
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="config"></param>
        public ZPayRefundApi(ZPayCenterConfig config=null) : base(config)
        {
        }

        /// <summary>
        /// 订单退款
        /// </summary>
        /// <param name="refundReq"></param>
        public async Task<ZPayRefundResp> RefunPayAsync(ZPayRefundReq refundReq)
        {
            const string respColumnName = "alipay_trade_refund_response";
            const string apiMethod = "alipay.trade.refund";

            return await PostApiAsync<ZPayRefundReq, ZPayRefundResp>(apiMethod, respColumnName, refundReq);
        }

    }
}
