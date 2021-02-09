using OrderingSystem.Business;
using OrderingSystem.Domain;
using OrderingSystem.IService;
using OrderingSystem.IService.ResponseModel;
using OrderingSystem.ZFB;
using OrderingSystem.ZFB.Pay;
using OrderingSystem.ZFB.Pay.Mos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrderingSystem.Service
{
    public class ZFBPayService:IZFBPayService
    {
        protected static ZPayTradeApi zPayApi = new ZPayTradeApi(ZFBPayConfig.config);

        /// <summary>
        /// The ActivityDiscount biz
        /// </summary>
        private IOrderBusiness _OrderBusinessBiz;

        public ZFBPayService(IOrderBusiness OrderBusinessBiz)
        {
            _OrderBusinessBiz = OrderBusinessBiz;
        }
        /// <summary>
        /// 支付生成签名
        /// </summary>
        /// <param name="placeAnOrderDto"></param>
        /// <returns></returns>
        public string ZPay(PostOrder order)
        {
            var payReq = new ZAddAppTradeReq(ZFBPayConfig.NotifyUrl);
            payReq.body = "H5下单支付";//string.IsNullOrWhiteSpace(placeAnOrderDto.Body) == true ? "一生时光下单支付" : placeAnOrderDto.Body;//placeAnOrderDto.Body;
            payReq.out_trade_no = order.order_no;//"s201212526123455221";
            payReq.total_amount = order.order_real_price;//总金额
            payReq.subject = "H5下单支付";//string.IsNullOrWhiteSpace(placeAnOrderDto.Subject) == true ? "一生时光下单支付" : placeAnOrderDto.Subject;//placeAnOrderDto.Subject;

            var res = zPayApi.GetAppTradeContent(payReq);
            return res.Data;
        }
        /// <summary>
        /// 支付宝支付回调
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        public string ZCallBack(ZPayCallBackResp resp, SortedDictionary<string, string> dir)
        {
            //验签
            var res = zPayApi.CheckCallBackSign(dir);
            //判断交易状态
            if (!resp.trade_status.ToUpper().Equals("TRADE_SUCCESS") || string.IsNullOrEmpty(resp.trade_no))
            {
                throw new Exception("交易失败;订单号:" + resp.out_trade_no);
            }
            //是否成功
            if (res.IsSuccess == true)
            {
                if (_OrderBusinessBiz.CheckOrder(resp.out_trade_no))
                {
                    _OrderBusinessBiz.PaymentCallback(resp.out_trade_no, resp.buyer_logon_id, resp.trade_no, (int)EnumHelp.PayType.微信);
                }
                else
                {
                    throw new Exception("支付失败;订单号：" + resp.out_trade_no + ";支付失败");
                }
            }
            throw new Exception("支付金额异常;订单号:" + resp.out_trade_no + ";支付金额:" + resp.total_amount + "元");
        }

        /// <summary>
        /// 获取回调所有参数
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = HttpContext.Current.Request.Form;
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            }
            return sArray;
        }
    }
}
