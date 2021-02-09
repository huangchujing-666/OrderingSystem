using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class PayDetailVM
    {
  
        public PayDetail PayDetail { get; set; } 

        public Paging<PayDetail> Paging { get; set; }
        

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
        public int QueryPayStatus { get; set; }
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