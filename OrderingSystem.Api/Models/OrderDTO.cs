using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderDTO
    {
        public int module { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public int order_id { get; set; }
        /// <summary>
        /// 商家Id
        /// </summary>
        public int business_id { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string business_name { get; set; }
        /// <summary>
        /// 商家图片id
        /// </summary>
        public string business_img_id { get; set; }
        /// <summary>
        /// 商家图片路径
        /// </summary>
        public string business_img_path { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string order_status { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 订单优惠后金额
        /// </summary>
        public string order_real_price { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_time { get; set; }

        /// <summary>
        /// 支付是否超时
        /// </summary>
        public string is_time_over { get; set; }

        /// <summary>
        /// 是否评论
        /// </summary>
        public int is_comment { get; set; }
    }
}