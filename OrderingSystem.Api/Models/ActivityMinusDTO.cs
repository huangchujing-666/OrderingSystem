using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class ActivityMinusDTO
    {
        /// <summary>
        /// 满减信息id
        /// </summary>
        public int activityminusid { get; set; }
        /// <summary>
        /// 满足金额
        /// </summary>
        public int achieve_amount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public int minus_amount { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public int business_id { get; set; }
    }
}