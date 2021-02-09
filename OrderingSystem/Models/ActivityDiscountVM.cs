using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderingSystem.Admin.Models;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 折扣视图类
    /// </summary>
    public class ActivityDiscountVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ActivityDiscountId { get; set; }
        public int BusinessInfoId { get; set; }


        /// <summary>
        /// 是否支持折扣
        /// </summary>
        public int IsSupport { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string Name { get; set; }
        public decimal? Discount { get; set; }

        public ActivityDiscount ActivityDiscount { get; set; }
        public List<ActivityMinus> ActivityMinusList { get; set; }

        public Paging<BusinessInfo> Paging { get; set; } 

        public string QueryName { get; set; }
        public int QueryType { get; set; }
    }
}