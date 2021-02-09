using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Models
{
    public class OrderExpert
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 下单人
        /// </summary>
        public string NickName { get; set; }
        
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNo { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }
    }
}