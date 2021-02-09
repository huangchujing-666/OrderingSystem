using OrderingSystem.IService.ResponseModel;
using OrderingSystem.ZFB.Pay.Mos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.IService
{
    /// <summary>
    /// 支付宝支付
    /// </summary>
    public interface IZFBPayService
    {
        /// <summary>
        /// 支付生成签名
        /// </summary>
        /// <param name="placeAnOrderDto"></param>
        /// <returns></returns>
        string ZPay(PostOrder order);

        /// <summary>
        /// 支付宝支付回调
        /// </summary>
        /// <returns></returns>
        string ZCallBack(ZPayCallBackResp resp, SortedDictionary<string, string> GetRequestPost);


        /// <summary>
        /// 获取回调所有参数
        /// </summary>
        /// <returns></returns>
        SortedDictionary<string, string> GetRequestPost();
    }
}
