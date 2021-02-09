using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.IService.ResponseModel
{

    /// <summary>
    /// 提交订单对象
    /// </summary>
    public class PostOrder
    {
        /// <summary>
        /// 1 食 2 衣 3 乐 4酒店 5 景点
        /// </summary>
        public int module { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int user_id { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int business_id { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal order_orign_price { get; set; }
        /// <summary>
        /// 订单优惠后金额
        /// </summary>
        public decimal order_real_price { get; set; }
        /// <summary>
        /// 1满减 2折扣
        /// </summary>
        public int discount_type { get; set; }

        /// <summary>
        /// 满减或折扣Id
        /// </summary>
        public int discount_id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 折扣优惠备注
        /// </summary>
        public string discount_remark { get; set; }
        /// <summary>
        /// 1微信 2支付宝
        /// </summary>
        public int pay_type { get; set; }
        /// <summary>
        /// 用户备注信息
        /// </summary>
        public string user_remark { get; set; }
        /// <summary>
        /// 订单详情列表
        /// </summary>
        public List<OrderDishesDTO> dishes_list { get; set; }


        public List<OrderProductDTO> product_list { get; set; }

        public OrderRoomDTO room_info { get; set; }
        /// <summary>
        /// 终端Ip
        /// </summary>
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 座位编号
        /// </summary>
        public int seat_no { get; set; }

        public string open_Id { get; set; }

        /// <summary>
        /// 景点门票信息
        /// </summary>
        public OrderTicketDTO ticket_info { get; set; }

        /// <summary>
        /// 套餐数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 使用开始时间
        /// </summary>
        public DateTime use_from_date { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime use_end_date { get; set; }
    }
    /// <summary>
    /// 提交菜品信息
    /// </summary>
    public class OrderDishesDTO
    {
        /// <summary>
        /// 菜品Id
        /// </summary>
        public int dishes_id { get; set; }
        /// <summary>
        /// 菜品数量
        /// </summary>
        public int dishes_count { get; set; }
        /// <summary>
        /// 原金额
        /// </summary>
        public decimal dishes_orign_price { get; set; }
        /// <summary>
        /// 实际价格
        /// </summary>
        public decimal dishes_real_price { get; set; }

        /// <summary>
        /// 菜品规格
        /// </summary>
        public string dishes_spec_detail_ids { get; set; }
    }

    public class OrderProductDTO
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public int product_id { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int product_count { get; set; }

        /// <summary>
        /// 产品原价格
        /// </summary>
        public decimal product_orign_price { get; set; }

        /// <summary>
        /// 产品实际价格
        /// </summary>
        public decimal product_real_price { get; set; }
    }

    public class OrderRoomDTO
    {
        /// <summary>
        /// 房间id
        /// </summary>
        public int room_id { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string phone_no { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string customer_name { get; set; }

        /// <summary>
        /// 原金额
        /// </summary>
        public decimal room_orign_price { get; set; }
        /// <summary>
        /// 实际价格
        /// </summary>
        public decimal room_real_price { get; set; }

    }

    public class OrderTicketDTO
    {
        /// <summary>
        /// 景点门票id
        /// </summary>
        public int ticket_id { get; set; }

        /// <summary>
        /// 更换房间差价
        /// </summary>
        public decimal minus_price { get; set; }
        /// <summary>
        /// 更换房间id
        /// </summary>
        public int room_id { get; set; }

        /// <summary>
        /// 酒店id
        /// </summary>
        public int hotel_id { get; set; }
        /// <summary>
        /// 是否更新房间
        /// </summary>
        public int update_room { get; set; }

        /// <summary>
        /// 门票使用顾客
        /// </summary>
        public string customer_ids { get; set; }
    }
}
