using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class WXPayDto
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        public string appId { get; set; }
        /// <summary>   
        ///    随机字符串 必填 String(32) 微信返回的随机字符串
        /// </summary>  
        public string nonce_str { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>   
        ///    预支付交易会话标识 必填 String(64) 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时
        /// </summary>  
        public string prepay_id { get; set; }
        /// <summary>
        /// 签名类型，只支持MD5
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string paySign { get; set; }
    }
}