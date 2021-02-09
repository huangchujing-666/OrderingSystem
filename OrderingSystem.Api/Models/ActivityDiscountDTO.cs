using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 折扣对象
    /// </summary>
    public class ActivityDiscountDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal discount { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int business_id { get; set; }
    }
}