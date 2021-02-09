using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BCOrderListDTO
    {
        /// <summary>
        /// 查询总订单数
        /// </summary>
        public int order_total_count { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int order_count { get; set; }
        /// <summary>
        /// 查询订单总金额
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 订单集合
        /// </summary>
        public List<BCOrderDTO> orderDto_list { get; set; }
    }

    public class BCOrderDTO
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public string order_time { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string order_statu { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public string real_amount { get; set; }
        /// <summary>
        /// 座位号
        /// </summary>
        public int seat_no { get; set; }

        public List<BCOrderDetailsDTO> order_details_list { get; set; }
    }

    public class BCOrderDetailsDTO
    {
        public string dishes_name { get; set; }

        public int count { get; set; }

        public string real_amount { get; set; }
    }
}