using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Models
{
    public class PayDetailExpert
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int PayDetailId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 支付金额
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
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PaySerialNo { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public string PayStatus { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; }

        public DateTime PayTime { get; set; }
    }
}