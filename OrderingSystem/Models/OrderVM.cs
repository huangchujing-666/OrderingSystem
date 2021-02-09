using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class OrderVM
    {
        public int Id { get; set; }

        /// <summary>
        /// 当前查询总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        public Order Order { get; set; }

        public List<OrderDetail> OrderDetailList { get; set; }
        public List<BusinessPayConfig> BusinessPayConfigList { get; set; }

        public Paging<Order> Paging { get; set; }

        // public string QueryName { get; set; }

        //查询条件
        /// <summary>
        /// 商家名称
        /// </summary>
        public string QueryBusinessName { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string QueryOrderNo { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string QueryUserName { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int QueryOrderStatusId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string QueryStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string QueryEndTime { get; set; }

    }
}