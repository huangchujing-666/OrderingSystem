using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class ActivityMinusVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ActivityMinusId { get; set; }

        /// <summary>
        /// 查询商家的Id
        /// </summary>
        public int BusinessInfoId { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 满减对象
        /// </summary>

        public ActivityMinus ActivityMinus { get; set; }

        public Paging<ActivityMinus> Paging { get; set; }

        public string QueryName { get; set; }
    }
}