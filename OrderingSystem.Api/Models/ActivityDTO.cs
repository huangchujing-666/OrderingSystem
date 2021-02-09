using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class ActivityDTO
    {
        /// <summary>
        /// 商家id
        /// </summary>
        public int business_id { get; set; }
        /// <summary>
        /// 满减集合
        /// </summary>
        public List<ActivityMinusDTO> minus { get; set; }

        /// <summary>
        /// 折扣对象
        /// </summary>
        public ActivityDiscountDTO discount { get; set;  }
    }
}