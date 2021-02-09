using OrderingSystem.IService.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderDetailDTO
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int order_id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
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
        /// 订单优惠后金额
        /// </summary>
        public string order_orign_price { get; set; }
        /// <summary>
        /// 订单优惠后金额
        /// </summary>
        public string order_real_price { get; set; }

        /// <summary>
        /// 实际价格减少金额
        /// </summary>
        public string minus_amount { get; set; }

        /// <summary>
        /// 订单优惠金额
        /// </summary>
        public string order_discount_price { get; set; }

        /// <summary>
        /// 商家电话
        /// </summary>
        public string business_mobile { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string pay_type { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public string pay_status { get; set; }

        /// <summary>
        /// 订单备注信息
        /// </summary>
        public string remark_message { get; set; }

        /// <summary>
        /// 订单折扣信息
        /// </summary>
        public string discount_remark { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_time { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 订单菜品详情
        /// </summary>
        public List<List<DishesDTO>> dishes_list { get; set; }

        /// <summary>
        /// 订单菜品详情
        /// </summary>
        public List<ProductDTO> product_list { get; set; }

        /// <summary>
        /// 座位号
        /// </summary>
        public int seat_no { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int activity_type { get; set; }

        /// <summary>
        /// 商家地址
        /// </summary>
        public string address { get; set; }

        public string is_time_over { get; set; }

        #region 酒店
        public RoomDTO roomdto { get; set; }


        /// <summary>
        /// 入住几晚
        /// </summary>
        public int night_count { get; set; }
        #endregion

        public TicketDTO ticketdto { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string use_start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string use_end_time { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int count { get; set; }

    }
}